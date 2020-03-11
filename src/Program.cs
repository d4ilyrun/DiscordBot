using System.IO;
using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using DiscordBot.Private;

namespace DiscordBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandHandler _handler;

        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Directory.SetCurrentDirectory("src");
            Console.WriteLine(Directory.GetCurrentDirectory());
            GoogleDriveAPI.GoogleAPI.UploadImageFromLink("https://cdn.discordapp.com/attachments/459779887767486484/686982257176477773/ESvVRVFUwAAtI4e.png");
        }
            //=> new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var config = new DiscordSocketConfig {MessageCacheSize = 100};
            _client = new DiscordSocketClient(config); // Connection au client
            _handler = new CommandHandler(_client);
            await _handler.InstallCommandsAsync();

            string token = APIKeys.BotAPI;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.MessageUpdated += MessageUpdated;
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                return Task.CompletedTask;
            };
                
            await (Task.Delay(-1));
        }
        
        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }
    }
}