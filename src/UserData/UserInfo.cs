using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private string _file = "users.json";
        private DiscordSocketClient _client;

        public UserInfo(DiscordSocketClient client)
        {
            _client = client;
        }

        // Returns the data about a given user
        public async Task<int> GetUserDataAsync(int discordID)
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
        public async Task StoreUserDataAsync(Dictionary<int, int> data)
        {
            Dictionary<int, int> content = JsonConvert.DeserializeObject<Dictionary<int, int>>(_file);
            
            foreach (var pair in data) {
                if(!content.ContainsKey(pair.Key))
                    content.Add(pair.Key, pair.Value);
            }

            JsonConvert.DeserializeObject < Dictionary<int, int>>(_file);
        }

        // Adds a user into the base, replaces the current value if it already exists
        public async Task UpdateUserData(Dictionary<int, int> data)
        {
            Dictionary<int, int> content = JsonConvert.DeserializeObject<Dictionary<int, int>>(_file);
            
            foreach (var pair in data) {
                if(content.ContainsKey(pair.Key)) content.Remove(pair.Key);
                content.Add(pair.Key, pair.Value);
            }
        }
    }
}






















