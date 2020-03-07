using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DiscordBot.Private;

namespace DiscordBot.OsuAPI
{
    public class OsuAPI
    {
        private static HttpClient HttpClient { get; set; }
        private static readonly string ApiKey = APIKeys.OsuAPI;

        private static void InitClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://osu.ppy.sh/api/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Gets information about something through the web API
        private static async Task<HttpResponseMessage> MakeRequest(string request, (string, string)[] args)
        {
            InitClient();
            
            if (args.Length > 0) {
                request += "?";
                foreach (var tuple in args)
                    request += $"{tuple.Item1}={tuple.Item2}&";
                request += $"k={ApiKey}";
            }
            
            return await HttpClient.GetAsync(request);
        }
        
        // Retrieves information about a player
        public static async Task<PlayerModel> RequestUser(string username)
        {
            (string, string)[] args = {("u", username)};
            
            using (HttpResponseMessage reponse = await MakeRequest("get_user", args)) {
                if (reponse.IsSuccessStatusCode) {
                    PlayerModel[] player = await reponse.Content.ReadAsAsync<PlayerModel[]>();
                    return player[0];
                }
                
                throw new Exception(reponse.ReasonPhrase);
            }
        }
        
        // Retrieves information about a beatmap
        public static async Task<BeatmapModel> RequestBeatmap(string beatmapsetID, string beatmapID = "")
        {
            (string, string)[] args = (beatmapID == "") 
                ? new (string, string)[] {("s", beatmapsetID)} 
                : new [] {("s", beatmapsetID), ("b", beatmapID)};

            using (HttpResponseMessage reponse = await MakeRequest("get_beatmaps", args)) {
                if (reponse.IsSuccessStatusCode) {
                    
                    BeatmapModel beatmap = await reponse.Content.ReadAsAsync<BeatmapModel>();
                    return beatmap;
                }
                
                throw new Exception(reponse.ReasonPhrase);
            }
        }
    }
}