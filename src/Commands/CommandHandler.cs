using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Schema;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.GoogleDriveAPI;

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
            SocketCommandContext context;
            int argPos = 0; // Delimiteur commande/parametres
            string[] imagesUrl = {".jpg", ".png"};

            // Verifie que ce soit un message d'utilisateur, et adressé au bot
            if (message == null
                || !(message.HasCharPrefix('!', ref argPos))
                || message.HasMentionPrefix(_client.CurrentUser, ref argPos)
                || message.Author.IsBot){
                    
                if (messageParam.Attachments.Count > 0) {
                    List<Attachment> images = message.Attachments
                    .Where(x => imagesUrl
                        .Any(y => x.Url.EndsWith(y)))
                    .ToList();

                    string channelID = messageParam.Channel.Id.ToString();
                    foreach(var image in images)
                        GoogleAPI.UploadImageFromLink(channelID, image.Url);
                }
                return;
            }
                
            // Detecte puis execute la commande
            context = new SocketCommandContext(_client, message);
            await _commands.ExecuteAsync(context, argPos, null);
        }
    }
}