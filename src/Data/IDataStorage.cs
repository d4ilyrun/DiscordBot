using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBot.UserData
{
    // Interface for storage services
    public interface IDataStorage
    {
        Task<List<KeyValuePair<string, object>>> GetDataAsync(string key);
        Task StoreDataAsync(Dictionary<string, object> data);
        Task UpdateDataAsync(Dictionary<string, object> data);
        Task DeleteDataAsync(string dataKey);
    }
}