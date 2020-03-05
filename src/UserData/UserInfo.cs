using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace DiscordBot.UserData
{
    public class UserInfo
    {
        private static string _file = "../../../UserData/users.json";

        // Returns the data about a given user
        public static async Task<int> GetUserDataAsync(int discordID)
        {
            Dictionary<int, int> content = JsonConvert.DeserializeObject<Dictionary<int, int>>(_file);
            List<KeyValuePair<int, int>> users = content.Where(x => x.Key == discordID).ToList();

            switch (users.Count) {
                case 0: // No user found
                    return -1;
                case 1: // User found
                    return users[0].Value;
                default: // Exception case
                    return -2;
            }
        }

        // Adds a user into the base, does nothing if it already exists
        public static async Task StoreUserDataAsync(Dictionary<int, int> data)
        {
            Console.WriteLine(File.Exists(_file));
            Dictionary<int, int> dico = JsonConvert.DeserializeObject<Dictionary<int, int>>(_file);
            //Dictionary<string, string> dico = JsonConvert.DeserializeObject<Dictionary<string, string>>(_file);
            Console.WriteLine("Desarialization Done.");
            //Dictionary<int, int> content = dico.ToDictionary(x => int.Parse(x.Key), x => int.Parse(x.Value));
            Console.WriteLine("All Done.");

            foreach (var pair in data) {
                Console.WriteLine($"{pair.Key} : {pair.Value}");
                if(!dico.ContainsKey(pair.Key))
                    dico.Add(pair.Key, pair.Value);
            }

            JsonConvert.DeserializeObject < Dictionary<int, int>>(_file);
        }

        // Adds a user into the base, replaces the current value if it already exists
        public static async Task UpdateUserData(Dictionary<int, int> data)
        {
            Dictionary<int, int> content = JsonConvert.DeserializeObject<Dictionary<int, int>>(_file);
            
            foreach (var pair in data) {
                if(content.ContainsKey(pair.Key)) content.Remove(pair.Key);
                content.Add(pair.Key, pair.Value);
            }
        }
    }
}






















