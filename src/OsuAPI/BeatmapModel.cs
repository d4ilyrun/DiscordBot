using Newtonsoft.Json;

namespace DiscordBot.OsuAPI
{
    public class BeatmapModel
    {
        public int beatmapset_id { get; set; }
        public int? beatmap_id { get; set; }
        public int? approved { get; set; }
        public int? total_length { get; set; }
        public string title { get; set; }
        public string artist { get; set; }
        public string version { get; set; } 
        public string creator { get; set; }
        public int? creator_id { get; set; }
        public float difficultyrating { get; set; }
        public int? bpm { get; set; }


        public override bool Equals(object obj)
        {
            BeatmapModel cible = obj as BeatmapModel;
            return !(cible == null ||
                     (cible.beatmap_id == this.beatmap_id 
                      && cible.beatmapset_id == this.beatmapset_id
                      ));
        }
    }
}