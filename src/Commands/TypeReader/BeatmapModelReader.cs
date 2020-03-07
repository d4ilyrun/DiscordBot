using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace DiscordBot.Commands.TypeReader
{
    public class BeatmapModelReader : Discord.Commands.TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            throw new NotImplementedException();
        }
    }
}