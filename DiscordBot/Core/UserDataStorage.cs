﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Core.UserAccounts;
using Newtonsoft.Json;


namespace DiscordBot.Core
{
    public static class UserDataStorage
    {
        //save all UserAccountList
        public static void SaveUserAccounts(IEnumerable<UserAccount> accounts, string filePath)
        {
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        //get all userAccounts
        public static IEnumerable<UserAccount> LoadUserAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            string json = File.ReadAllText(filePath);

            //want to deserialize 
            return JsonConvert.DeserializeObject<List<UserAccount>>(json);


        }

        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
