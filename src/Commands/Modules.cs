using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.UserData;

namespace DiscordBot.Commands
{
    public class infoModules : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echoes a message")]
        public Task SayAsync([Remainder] string echo)
            => ReplyAsync(echo);
    }

    [Group("data")]
    public class dataModules : ModuleBase<SocketCommandContext>
    {
        [Command("register")]
        [Alias("rg")]
        [Summary("Saves data about a user into the base")]
        public Task registerUserAsync(int discordID, int osuID)
           => UserInfo.StoreUserDataAsync(new Dictionary<int, int>() {{discordID, osuID}});


        [Command("get")]
        [Summary("Gets data about a user from the base")]
        public Task getUserAsync(int discordID)
            => UserInfo.GetUserDataAsync(discordID);
    }
    
    [Group("osu")]
    public class osuModules : ModuleBase<SocketCommandContext>
    {
        
    }
}