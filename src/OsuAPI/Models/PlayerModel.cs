using System;
using System.Collections.Generic;
using Discord;

namespace DiscordBot.OsuAPI
{
    public class PlayerModel
    {
        public int? user_id { get; set; }
        public string username { get; set; }
        public string country { get; set; }
        public float? level { get; set; }
        public float? accuracy { get; set; }
        public int? playcount { get; set; }
        public float? pp_raw { get; set; }
        public int? pp_rank { get; set; }
        public int? pp_country_rank { get; set; }
        public int? count_rank_ss { get; set; }
        public int? count_rank_ssh { get; set; }
        public int? count_rank_s { get; set; }
        public int? count_rank_sh { get; set; }
        public int? count_rank_a { get; set; }


        public override bool Equals(object obj)
        {
            PlayerModel cible = obj as PlayerModel;
            return !(cible == null || cible.user_id == this.user_id );
        }


        public Embed DiscordEmbed()
        {
            var embed = new EmbedBuilder()
            {
                Color = Color.DarkMagenta,
                ThumbnailUrl = $"https://s.ppy.sh/a/{this.user_id}",
                Author = new EmbedAuthorBuilder()
                {
                    Name = $"{this.username}",
                    Url = $"https://osu.py.sh/users/{this.username}"
                },
                Fields = new List<EmbedFieldBuilder>(2)
                {
                    new EmbedFieldBuilder()
                    {
                        Name = "Performance",
                        IsInline   = true,
                        Value = $@"
**--- {this.pp_raw}pp**
**Global Rank:** #{this.pp_rank}(:flag_{this.country.ToLower()}:: #{this.pp_country_rank})
**Accuracy:** {this.accuracy}%
**Play count:** {this.playcount}
**Level:** {this.level}
"
                    },
                    new EmbedFieldBuilder()
                    {
                        Name = "Rank",
                        IsInline = true,
                        Value = $@"
**SS**: {this.count_rank_ss + this.count_rank_ssh}
**S**: {this.count_rank_s + this.count_rank_sh}
**A**: {this.count_rank_a}
"
                    }
                }
            };

            return embed.Build();
        }
    }
}