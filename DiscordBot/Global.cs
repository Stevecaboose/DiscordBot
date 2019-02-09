using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot
{
    internal static class Global
    {
        internal static DiscordSocketClient client { get; set; }
        internal static ulong MessageIDToTrack { get; set; }

    }
}
