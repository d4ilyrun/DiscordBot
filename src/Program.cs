using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace DiscordBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;
        
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
           _client = new DiscordSocketClient(); // Connection au client
           _client.Log += Log;
           _handler = new CommandHandler(_client);

           // Temporary => Unsecure
           string token = "Mzc4OTMxNjcwMDg4Mjg2MjA4.Xl_pMw.obYH4Y0LgWHGeKN4LHyjZ2dvyt8";

           await _client.LoginAsync(TokenType.Bot, token);
           await _client.StartAsync();
           
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