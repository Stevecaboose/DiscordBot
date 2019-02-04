using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;

/**
 * Class that holds misc commands
 * Attributes hold the command
 */

namespace DiscordBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        //display all of the commands
        [Command("help")]
        public async Task Help()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("List of commands");
            embed.WithDescription(PrintHelp());
            embed.WithColor(0, 0, 255);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        //display all of the commands for admins
        [Command("helpAdmin")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task HelpAdmin()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("List of admin commands");
            embed.WithDescription(PrintAdminHelp());
            embed.WithColor(20, 30, 140);

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }



        
        //add xp to admin accounts (used for testing)
        [Command("addxp")]
        [RequireUserPermission(GuildPermission.Administrator)] //must be admin to execute command
        public async Task AddXP(IGuildUser user, uint xp)
        {
            var account = UserAccountList.GetAccount(user as SocketUser);
            account.XP += xp;
            UserAccountList.SaveAccounts();
            await Context.Channel.SendMessageAsync($"{user.Username} gained {xp} xp.");
        }

        //echo command
        [Command("echo")]
        public async Task Echo([Remainder] string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Echoed message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }


        //george command
        // this is when george needs help with his homework
        [Command("george")]
        public async Task George()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Hey guys");
            embed.WithDescription("Could you guys send me your homework?");
            embed.WithColor(new Color(45, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        //pick command
        // randomly selects an option in message. Each option must be separated by a | character
        [Command("pick")]
        public async Task PickOne([Remainder] string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            if (options.Length == 1)
            {
                var exceptEmbed = new EmbedBuilder();
                exceptEmbed.WithTitle("Sorry try again");
                exceptEmbed.WithDescription("Make sure to use more than one option separated by a |");
                exceptEmbed.WithColor(255, 0, 0);
                await Context.Channel.SendMessageAsync("", false, exceptEmbed.Build());
            }
            else
            {
                Random r = new Random();
                string selection = options[r.Next(0, options.Length)];

                var embed = new EmbedBuilder();
                embed.WithTitle("Choice for " + Context.User.Username);
                embed.WithDescription(selection);
                embed.WithColor(new Color(0, 255, 0));
                embed.WithThumbnailUrl("http://trainbeyondthebox.com/wp-content/uploads/2018/03/dice.jpg");



                await Context.Channel.SendMessageAsync("", false, embed.Build());
                DataStorage.AddPairToStorage(Context.User.Username + " " + DateTime.Now.ToLongTimeString(), selection); //save the choice to the DataStorage

            }


        } // end pick command

        [Command("secret")]
        public async Task RevealSecret([Remainder] string arg = "")
        {
            //check to make sure the user has the role "SecretOwner"
            if (!UserIsSecretOwner((SocketGuildUser)Context.User)) //cast Context as SocketGuildUser
            {
                await Context.Channel.SendMessageAsync(":x: You need the SecretOwner role to do that. " + Context.User.Mention);
                return;
            }

            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }

        private bool UserIsSecretOwner(SocketGuildUser user)
        {
            string targetRoleName = "SecretOwner";

            // query to get the roles that match the target role name
            // this may be slow. Because it has to go through all the roles each time
            var result = from r in user.Guild.Roles
                        where r.Name == targetRoleName
                        select r.Id;

            ulong roleID = result.FirstOrDefault();

            if (roleID == 0) //this doesn't mean the user doesn't have the role, it means we didn't find that role (aka error)
            {
                Console.WriteLine(roleID.ToString() + "(role error)");
                return false;
            }
            
            //we have found the role if we got to this point

            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);

        }

        [Command("data")]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync("Data Has " + DataStorage.GetPairsCount() + " pairs.");
            DataStorage.AddPairToStorage("Count" + DataStorage.GetPairsCount(),
                "TheCount" + DataStorage.GetPairsCount());
        }

        [Command("stats")]
        public async Task Stats(IGuildUser user)
        {
            var account = UserAccountList.GetAccount(user as SocketUser);
            await Context.Channel.SendMessageAsync($"{user.Username} has {account.XP} XP and {account.Points} points.");
        }




        private string PrintHelp()
        {
            const string message = "echo <message> Echos the same message\n" +
                                   "pick <choice1>|<choice2>|... Randomly selects one of the choices separated by a |\n" +
                                   "george See how George is doing\n" +
                                   "secret See what the secret message is\n" +
                                   "data check data\n" +
                                   "stats <@username> check XP and Points for a particular user\n" +
                                   "helpAdmin Displays the help menu for admins";

            return message;
        }

        private string PrintAdminHelp()
        {
            const string message = "addxp <@username> <<integer amount> adds an amount of xp to the selected user.";

            return message;
        }
    }
}
