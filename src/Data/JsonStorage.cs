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
    // A class made to handle generic Dictionnary type json files
    public class JsonStorage : IDataStorage
    {
        private static string _file;

        public JsonStorage(string file)
        {
            _file = file;
        }

        public  Dictionary<string, object> GetDictionnary()
            => JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(_file));
        
        // Returns the data corresponding to the key
        public Task<List<KeyValuePair<string, object>>> GetDataAsync(string key)
        {
            Dictionary<string, object> dict = GetDictionnary();
            List<KeyValuePair<string, object>> res = dict.Where(x => x.Key.Equals(key)).ToList();
            
            return Task.FromResult(res);
        }

        // Adds a dataset into the base, does nothing if it already exists
        public Task StoreDataAsync(Dictionary<string, object> data)
        {
            Dictionary<string, object> dict = GetDictionnary();
            
            foreach (var pair in data) 
                dict.TryAdd(pair.Key, pair.Value);
            
            string json = JsonConvert.SerializeObject(dict, Formatting.Indented);
            File.WriteAllText(_file, json);

            return Task.CompletedTask;
        }

        // Adds a dataset into the base, replaces the current value if it already exists
        public Task UpdateDataAsync(Dictionary<string, object> data)
        {
            Dictionary<string, object> content = GetDictionnary();
            
            foreach (var pair in data) {
                if(content.ContainsKey(pair.Key)) content.Remove(pair.Key);
                content.Add(pair.Key, pair.Value);
            }
            
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);
            File.WriteAllText(_file, json);
            
            return Task.CompletedTask;
        }
        
        // Deletes a set of data from the database
        public Task DeleteDataAsync(string dataKey)
        {
            Dictionary<string, object> content = GetDictionnary();
            content.Remove(dataKey);

            string json = JsonConvert.SerializeObject(content, Formatting.Indented);
            File.WriteAllText(_file, json);
            
            return Task.CompletedTask;
        }
    }
}