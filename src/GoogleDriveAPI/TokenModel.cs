using System;

namespace DiscordBot.GoogleDriveAPI
{
    class TokenModel 
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string issued { get; set; }
        public string issuedUtc { get; set; }
    }
}