using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Commands
{
    public class infoModules : ModuleBase<SocketCommandContext>
    {
        // !say hello -> "hello"
        [Command("say")]
        [Summary("Echoes a message")]
        public Task SayAsync([Remainder] string echo)
            => ReplyAsync(echo);
    }
    
    [Group("osu")]
    public class osuModules : ModuleBase<SocketCommandContext>
    {
        
    }
}