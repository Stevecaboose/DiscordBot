using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord.WebSocket;

namespace DiscordBot.Core
{
    internal static class RepeatingTimer
    {

        private static Timer loopingTimer;
        private static SocketTextChannel channel;

        internal static Task StartTimer()
        {
            //Global.client.GetGuild(538039404544000031).GetTextChannel(538039404544000033);
            channel = Global.client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.textChannel);
            loopingTimer = new Timer()
            {
                 Interval = 5000,
                 AutoReset = true,
                 Enabled = true
            };

            loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        //what to do when the timer is up
        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {


           // await channel.SendMessageAsync("Ping!");
        }
    }
}
