using System;

namespace DiscordBot.Core.UserAccounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }

        public uint Points { get; set; }

        public uint XP { get; set; }

        public bool TimeOutFromXPGain { get; set; }

        public uint LevelNumber
        {
            get
            {
                // y = x^2 * 50. Where x is the level and y is the xp
                return (uint) Math.Sqrt(XP / 50); // get level from xp points
            }
        }
    }
}
