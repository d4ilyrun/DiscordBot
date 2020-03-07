using System;
using System.Threading.Tasks;
using Discord;

namespace DiscordBot.OsuAPI
{
    public class OsuBot
    {
        public static async Task<Embed> PrintPlayer(string username)
        {
            PlayerModel player = await OsuAPI.RequestUser(username);
            return (player == null)
                    ? new EmbedBuilder().WithTitle("**Error:** No user found").WithColor(Color.Red).Build()
                    : player.DiscordEmbed();
        }
    }
}
