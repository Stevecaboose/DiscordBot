using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private static int count = 0;

        internal static Task StartTimer()
        {
            //Global.client.GetGuild(538039404544000031).GetTextChannel(538039404544000033);
            channel = Global.client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.textChannel);
            loopingTimer = new Timer()
            {
                 Interval = 60000,
                 AutoReset = true,
                 Enabled = false //switch this if you decide to use the timer
            };

            loopingTimer.Elapsed += OnTimerTicked;

            return Task.CompletedTask;
        }

        //what to do when the timer is up
        private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
        {

            

        }
    }
}
