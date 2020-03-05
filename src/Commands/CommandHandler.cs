using System;
using System.Net.Security;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commands;

        public CommandHandler(DiscordSocketClient client)
        {
            this._client = client;
            this._commands = new CommandService();
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {        
            var message = messageParam as SocketUserMessage;
            int argPos = 0; // Delimiteur commande/parametres

            // Verifie que ce soit un message d'utilisateur, et adressé au bot
            if (message == null
                || !(message.HasCharPrefix('!', ref argPos))
                || message.HasMentionPrefix(_client.CurrentUser, ref argPos)
                || message.Author.IsBot)
                return;
            
            // Detecte puis execute la commande
            var context = new SocketCommandContext(_client, message);
            await _commands.ExecuteAsync(context, argPos, null);
        }
    }
}