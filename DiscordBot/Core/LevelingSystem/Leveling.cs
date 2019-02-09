using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;


namespace DiscordBot.Core.LevelingSystem
{
    internal static class Leveling
    {
        internal static async void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            //if the user has a timeout, ignore them

            var userAccount = UserAccounts.UserAccountList.GetAccount(user);

            uint oldLevel = userAccount.LevelNumber;

            var xpGainTimer = new Timer()
            {
                Interval = 300000,
                Enabled = true,
                AutoReset = true
            };



            xpGainTimer.Elapsed += (sender, e)  => XpGainTimer_Elapsed(sender, e, user);

            if (!userAccount.TimeOutFromXPGain)
            {
                userAccount.XP += 50;
                userAccount.TimeOutFromXPGain = true;
            }
            else
            {
                //do nothing
            }


            UserAccounts.UserAccountList.SaveAccounts();
            uint newLevel = userAccount.LevelNumber;

            if (oldLevel != newLevel)
            {
                // if not the same level, then the user leveled up
                var embed = new EmbedBuilder();
                embed.WithColor(67, 160, 71);
                embed.WithTitle("LEVEL UP");
                embed.WithDescription(user.Username + " just leveled up!");
                embed.AddField("LEVEL", newLevel);
                embed.AddField("XP", userAccount.XP);

                await channel.SendMessageAsync("", false, embed.Build());
            }
        }

        //if we are inside the time limit, add the xp 
        private static void XpGainTimer_Elapsed(object sender, ElapsedEventArgs e, SocketGuildUser user)
        {
            var userAccount = UserAccounts.UserAccountList.GetAccount(user);

            //if the user has a timeout, reset it because the timer is up
            if (userAccount.TimeOutFromXPGain)
            {
                
                userAccount.TimeOutFromXPGain = false;
            }
            
        }
    }
}
