using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Instant.Tasks
{
    internal class ImgurTask : ITask
    {
        private readonly string Endpoint = "https://api.imgur.com/3/upload/";
        private readonly string ClientId = Secrets.ImgurClientId;

        private readonly HttpClient http;

        public ImgurTask()
        {
            http = new HttpClient();
        }

        public async void Execute(Snapshot snapshot)
        {
            using var ms = new MemoryStream();
            snapshot.Image.Save(ms, ImageFormat.Png);

            var request = new HttpRequestMessage(HttpMethod.Post, Endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Client-ID", ClientId);
            request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("image", Convert.ToBase64String(ms.ToArray()))
            });

            var response = await http.SendAsync(request);
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var link = JObject.Parse(content)["data"].SelectToken("link").ToString();
                Clipboard.SetText(link);

                var processInfo = new ProcessStartInfo(link)
                {
                    UseShellExecute = true,
                    Verb = "open"
                };

                Process.Start(processInfo);
            }
        }
    }
}