using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.UserData;

namespace DiscordBot.Commands
{
    public class InfoModules : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echoes a message")]
        public Task SayAsync([Remainder] string echo)
            => ReplyAsync(echo);
    }

    [Group("data")]
    public class DataModules : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        [Summary("Saves data about a user into the base")]
        public Task RegisterUserAsync(string discordID, int osuID)
            => UserInfo.StoreUserAsync(new Dictionary<string, int>{{discordID, osuID}});
        
        
        [Command("update")]
        [Summary("Update data about a user in the base")]
        public Task UpdateUserAsync(string discordID, int osuID)
            => UserInfo.UpdateUserAsync(new Dictionary<string, int>{{discordID, osuID}});


        [Command("delete")]
        [Summary("Gets data about a user from the base")]
        public Task GetUserAsync(string discordID)
            => UserInfo.DeleteUserAsync(discordID);
    }
    
    [Group("osu")]
    public class OsuModules : ModuleBase<SocketCommandContext>
    {
        
    }
}