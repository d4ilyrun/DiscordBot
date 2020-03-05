﻿using System;
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
            var config = new DiscordSocketConfig {MessageCacheSize = 100};
            _client = new DiscordSocketClient(config); // Connection au client
            _handler = new CommandHandler(_client);
            await _handler.InstallCommandsAsync();

            // Temporary => Unsecure
            string token = "Mzc4OTMxNjcwMDg4Mjg2MjA4.Xl_pMw.obYH4Y0LgWHGeKN4LHyjZ2dvyt8";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.MessageUpdated += MessageUpdated;
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                return Task.CompletedTask;
            };
                
            //Blocks the task until the program is closed
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