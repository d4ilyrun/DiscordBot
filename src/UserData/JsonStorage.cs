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
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace DiscordBot.UserData
{
    public abstract class JsonStorage : IDataStorage
    {
        private static string _file;

        public static Dictionary<string, object> GetDictionnary()
            => JsonConvert.DeserializeObject<Dictionary<string, Object>>(_file);
        
        
        // Returns the data corresponding to the key
        public static async Task<List<KeyValuePair<string, object>>> GetDataAsync(string key)
        {
            Dictionary<string, object> dict = GetDictionnary();
            List<KeyValuePair<string, object>> res = dict.Where(x => x.Key == key).ToList();

            return res;
        }

        // Adds a dataset into the base, does nothing if it already exists
        public static async Task StoreDataAsync(Dictionary<string, object> data)
        {
            Dictionary<string, object> dict = GetDictionnary();
            
            foreach (var pair in data) {
                dict.TryAdd(pair.Key, pair.Value);
            }
        }

        // Adds a user into the base, replaces the current value if it already exists
        public static async Task UpdateDataAsync(Dictionary<string, object> data)
        {
            Dictionary<string, object> content = GetDictionnary();
            
            foreach (var pair in data) {
                if(content.ContainsKey(pair.Key)) content.Remove(pair.Key);
                content.Add(pair.Key, pair.Value);
            }
        }
    }
}






















