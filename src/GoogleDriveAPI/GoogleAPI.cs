using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using File = Google.Apis.Drive.v3.Data.File;
using DiscordBot.UserData;
using Newtonsoft.Json;

namespace DiscordBot.GoogleDriveAPI
{
    public class GoogleAPI
    {
        private static string[] _scopes = { DriveService.Scope.Drive };
        private static readonly string _applicationName = "DiscordBot";
        private static UserCredential _credential = GetCredentials();
        private static DriveService _service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = _credential,
            ApplicationName = _applicationName,
        });


        private static UserCredential GetCredentials()
        {
            UserCredential credential;
            
            using (var stream =
                new FileStream("Data/Database/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;
        }


        public static void UploadImageFromLink(string channelID, string link)
        {
            // Only permitted channels can save their images onto the drive
            if(!Enum.IsDefined(typeof(Whitelist), long.Parse(channelID)))
                return;

            channelID = "ABSOLUTE"; // Used when there is only one token    
            string name = RadomizeName(10) + ".jpg";
            LoadToken(channelID);
            File fileMetadata = CreateFile(channelID, name);

            using (var client = new WebClient()){
                client.DownloadFile(link, name);
            }

            UploadImage(channelID, name, fileMetadata);
            System.IO.File.Delete(name);
        }
        
        
        public static void UploadImage(string channelID, string path, File fileMetadata)
        {
            FilesResource.CreateMediaUpload request;
            LoadToken(channelID);

            using (var stream = new System.IO.FileStream(path,
                System.IO.FileMode.Open))
            {
                request = _service.Files.Create(
                    fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;
            string test = (file == null) ? "null":file.Id;
            Console.WriteLine(test);

            GoogleAPI.SaveToken(channelID);
        }   


        private static string RadomizeName(int length)
        {
            string res = "";
            Random random = new Random();
            
            for (int i = 0; i < length; i++)
                res += random.Next(10).ToString();
            return res;
        }
   
        // Can be used in case you use multiple tokens (originally planned but not implemented yes)
        private static File CreateFile(string channelID, string name)
        {
            Dictionary<string, TokenModel> dict = JsonConvert.DeserializeObject<Dictionary<string, TokenModel>>(
                System.IO.File.ReadAllText("Data/Database/googleTokens.json"));
            bool hasValue = dict.TryGetValue(channelID, out TokenModel token);

            File file = new File() { Name = name };
            if(hasValue && token.folderId != null)
                file.Parents = new List<string> { token.folderId };

            return file;
        }

        // Can be used in case you use multiple tokens (originally planned but not implemented yes)
        private static void SaveToken(string channelID)
        {
            JsonStorage json = new JsonStorage("Data/Database/googleTokens.json");
            TokenModel token = JsonConvert.DeserializeObject<TokenModel>(
                System.IO.File.ReadAllText("token.json/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user")
            );

            json.StoreDataAsync(new Dictionary<string, object> {{channelID, token}});
            System.IO.File.WriteAllText("token.json/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user", "{}");
        }

        // Can be used in case you use multiple tokens (originally planned but not implemented yes)
        private static async void LoadToken(string channelID)
        {
            JsonStorage json = new JsonStorage("Data/Database/googleTokens.json");
            List<KeyValuePair<string, object>> list = await json.GetDataAsync(channelID);

            string token = (list.Count > 0) ? JsonConvert.SerializeObject(list[0].Value) : "{}";
            System.IO.File.WriteAllText("token.json/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user", token);
        }
    }
}