using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.OsuAPI;
using DiscordBot.UserData;
using DiscordBot.GoogleDriveAPI;

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
        public async Task RegisterUserAsync(string discordID, string username)
            => await UserInfo.StoreUserAsync(new Dictionary<string, PlayerModel>{{discordID, await OsuAPI.OsuAPI.RequestUser(username)}});
        
        
        [Command("update")]
        [Summary("Update data about a user in the base")]
        public async Task UpdateUserAsync(string discordID, string username)
            => await UserInfo.UpdateUserAsync(new Dictionary<string, PlayerModel>{{discordID, await OsuAPI.OsuAPI.RequestUser(username)}});


        [Command("delete")]
        [Summary("Gets data about a user from the base")]
        public Task GetUserAsync(string discordID)
            => UserInfo.DeleteUserAsync(discordID);
    }
    
    [Group("osu")]
    public class OsuModules : ModuleBase<SocketCommandContext>
    {
        [Command("u")]
        [Summary("Prints a summary of the player using discord's rich embed messages")]
        public async Task DisplayUser(string username)
            =>  await ReplyAsync("", false,  (Embed) await OsuBot.PrintPlayer(username));
    }


    [Group("image")]
    public class ImageModules : ModuleBase<SocketCommandContext>
    {
        [Command("save")]
        [Summary("saves an image (given by link) on a distant google drive")]
        public async Task SaveImageLink(string channelID, string imageLink)
            => await new Task ( () =>  GoogleAPI.UploadImageFromLink(channelID, imageLink) );
    }
}