using System.Net;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using System.Net.Http;

namespace Remote.Data
{
    public class RestService
    {
        public string? targetip { get; set; }

        public string? targetport { get; set; }

        public string? targetcommand { get; set; }

        public string? targetcommandresult { get; set; }




        public void Get(string path)
        {
            string jsonstring = "";
            HttpClientHandler httpClientHandler = new HttpClientHandler();

            using (var client = new HttpClient(httpClientHandler))
            {
                var endpoint = new Uri("http://" + this.targetip + ":" + this.targetport + path);
                var result = client.GetAsync(endpoint).Result;
                jsonstring = result.Content.ReadAsStringAsync().Result;
            }

            this.targetcommandresult = jsonstring;
        }

        public async void Post(string path)
        {
            string url = "http://" + this.targetip + ":" + this.targetport + path;

            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "command", this.targetcommand }
            };

            var data = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, data);
            
        }

        
    }


}