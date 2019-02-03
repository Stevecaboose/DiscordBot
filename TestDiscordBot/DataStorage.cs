using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestDiscordBot
{
    class DataStorage
    {
       private static Dictionary<string, string> pairs = new Dictionary<string, string>();

       public static void AddPairToStorage(string key, string value)
       {
           pairs.Add(key, value);
           SaveData();
       }

       public static int GetPairsCount()
       {
           return pairs.Count;
       }

       static DataStorage()
       {
            // load data
            if (!ValidateStorageFile("DataStorage.json"))
            {
                return;

            }

            string json = File.ReadAllText("DataStorage.json");


            pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        }


       public static void SaveData()
       {
           // save data
           string json = JsonConvert.SerializeObject(pairs, Formatting.Indented);
           File.WriteAllText("DataStorage.json", json);

       }

       private static bool ValidateStorageFile(string file)
       {
           if (!File.Exists(file))
           {
               File.WriteAllText(file, ""); // create the file and it's empty at this point
               SaveData();
               return false;
           }

           return true;
       }
    }


}
