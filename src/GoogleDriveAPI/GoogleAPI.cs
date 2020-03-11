using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
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
        private static string _applicationName = "DiscordBot";
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

        public static void UploadImageFromLink(string link)
        {
            using (var client = new WebClient())
            {
                //client.DownloadFile(link, RandomizeName(10));
            }
        }
        
        public static void UploadImage(string path)
        {
            var fileMetadata = new File()
            {
                Name = RadomizeName(9)
            };
            
            FilesResource.CreateMediaUpload request;
            Console.WriteLine(path);
            if (_service == null) Console.WriteLine("null");
            else Console.WriteLine("truc");
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

            GoogleAPI.SaveToken("test");
        }

        private static string RadomizeName(int length)
        {
            string res = "";
            Random random = new Random();
            
            for (int i = 0; i < length; i++)
                res += random.Next(10).ToString();
            return res;
        }

        private static void SaveToken(string userID)
        {
            JsonStorage json = new JsonStorage("Data/Database/googleTokens.json");
            TokenModel token = JsonConvert.DeserializeObject<TokenModel>(
                System.IO.File.ReadAllText("token.json/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user")
            );

            json.StoreDataAsync(new Dictionary<string, object> {{userID, token}});
        }

        private async void LoadToken(string userID)
        {
            JsonStorage json = new JsonStorage("Data/Database/googleTokens.json");
            TokenModel token = (await json.GetDataAsync(userID))[0].Value as TokenModel;
            string res = JsonConvert.SerializeObject(token);

            System.IO.File.WriteAllText("token.json/Google.Apis.Auth.OAuth2.Responses.TokenResponse-user", res);
        }
    }
}