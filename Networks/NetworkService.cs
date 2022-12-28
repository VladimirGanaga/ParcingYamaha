using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha.Networks
{
    public class NetworkService
    {
        HttpClient httpClient;

        public NetworkService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> PostRequest(string requestURL, string stringContent)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, requestURL);
            request.Content = new StringContent(stringContent, Encoding.UTF8, "application/json");
            request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
            var response = await httpClient.SendAsync(request);
            var answer = await response.Content.ReadAsStringAsync();
            return answer;

        }
    }
}
