using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var userAccount = UserAccounts.UserAccounts.GetAccount(user);

            uint oldLevel = userAccount.LevelNumber;
            userAccount.XP += 50;
            UserAccounts.UserAccounts.SaveAccounts();
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
    }
}
