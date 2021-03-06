﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.Core.UserAccounts
{
    class UserAccountList
    {

        private static List<UserAccount> accountList;

        public static List<UserAccount> GetList()
        {
            return accountList;
        }

        private static string accountsFile = "Resources/accounts.json";

        static UserAccountList()
        {
            //check if the accountsFile exists
            if (UserDataStorage.SaveExists(accountsFile))
            {
                accountList = UserDataStorage.LoadUserAccounts(accountsFile).ToList();

                //reset xp gain timers
                foreach (var account in accountList)
                {
                    account.TimeOutFromXPGain = false;
                }
            }
            else //we dont have a save file
            {
                accountList = new List<UserAccount>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            UserDataStorage.SaveUserAccounts(accountList, accountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
        {
            return GetOrCreateAccount(user.Id);
        }

        private static UserAccount GetOrCreateAccount(ulong id)
        {
            //search through the account list. If theres no account, we want to create one
            // use link queuery to search list

            var result = from a in accountList
                where a.ID == id
                select a;

            //if no account found, make a new one
            var account = result.FirstOrDefault() ?? CreateUserAccount(id);

            return account;

        }

        private static UserAccount CreateUserAccount(ulong id)
        {
            var newAccount = new UserAccount()
            {
                ID = id,
                Points = 0,
                XP = 0,
                TimeOutFromXPGain = false,
                LastMessageTimestamp = DateTime.Now
            };

            accountList.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }

        public static bool ResetXP()
        {
            foreach (var account in accountList)
            {
                account.XP = 0;
            }

            return true; //it made it through the loop with no issues
        }
    }
}
