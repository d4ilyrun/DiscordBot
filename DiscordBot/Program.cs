using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordBot
{
    class Program
    {
        private DiscordSocketClient _client;
        
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
           _client = new DiscordSocketClient(); // Connection au client
           _client.Log += Log;

           // Temporary => Unsecure
           string token = "j3M-wRMavvK049-TbNz8XxTAtfugdXJn";

           await _client.LoginAsync(TokenType.Bot, token);
           //Blocks the task until the program is closed
           await (Task.Delay(-1));
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}