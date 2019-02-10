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
            await RepeatingTimer.StartTimer();

            uint oldLevel = userAccount.LevelNumber;
            //string currentTimeString = DateTime.Now.ToString();
            DateTime currentTime = DateTime.Now;
            TimeSpan span = currentTime.Subtract(userAccount.LastMessageTimestamp);
            
            //if the difference in minutes is less than 2, do not allow the user to gain xp
            if (span.TotalMinutes < Config.bot.levelMessageTimeout)
            {
                userAccount.TimeOutFromXPGain = true; //no use right now. Should function without this
                // Maybe i can find a use for it later
            }
            else // otherwise, the use has waited at least 2 minutes
            {
                //give the user the xp
                userAccount.XP += Config.bot.XPGain;
                //reset the flag
                userAccount.TimeOutFromXPGain = false;
                //reset timestamp
                userAccount.LastMessageTimestamp = currentTime;

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


    }
}
