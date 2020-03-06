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
        private static JsonStorage _json = new JsonStorage("UserData/users.json");
        
        // Retrieves the osu profile linked to the account from the database
        public static async Task<List<KeyValuePair<string ,int>>> GetUserAsync(string key)
        {
            List<KeyValuePair<string, object>> dict = await _json.GetDataAsync(key);
            List<KeyValuePair<string, int>> res = dict.Select(x => new KeyValuePair<string, int>(x.Key, (int) x.Value)).ToList();
            return res;
        }

        // Stores a new profile into the database, does nothing if one is already linked to this discord account
        public static async Task StoreUserAsync(Dictionary<string, int> data)
        {
            Dictionary<string, object> dict = data.ToDictionary(x => x.Key, x => x.Value as object);
            await _json.StoreDataAsync(dict);
        }

        // Stores a new profile into the database, replaces its value if one is already linked to this discord account
        public static async Task UpdateUserAsync(Dictionary<string, int> data)
        {
            try {
                Dictionary<string, object> dict = data.ToDictionary(x => x.Key, x => x.Value as object);
                await _json.UpdateDataAsync(dict);
            }
            catch (Exception e) {
                Console.Write(e.Message);
            }
        }
        
        // Deletes a profile linked to a discord account from the database
        public static async Task DeleteUserAsync(string discordID)
        {
            await _json.DeleteDataAsync(discordID);
        }
    }
}