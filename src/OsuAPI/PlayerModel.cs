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
    }
}