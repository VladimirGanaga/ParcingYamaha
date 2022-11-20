using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace ParcingYamaha
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
           

            HttpClient httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/product_list/");
            request.Content = new StringContent("{\"baseCode\":\"7306\",\"langId\":\"92\"}", Encoding.UTF8, "application/json");
            request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
            //request.Headers.Add("", "");

                   
            var response = await httpClient.SendAsync(request);
            var answer = await response.Content.ReadAsStringAsync();
            var engineSize=JsonConvert.DeserializeObject<Engine>(answer);
            //var engineSize=JsonConvert.DeserializeObject(answer);
            
            foreach (var engine in engineSize.displacementDataCollection)
            {
                Console.WriteLine($"объем: {engine.displacement}, номер: {engine.displacementType}, ИД продукта: {engine.productId}");
                request=new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/");
                request.Content = new StringContent($"{{ \"productId\":\"10\",\"displacementType\":\"{engine.displacementType}\",\"baseCode\":\"7306\",\"langId\":\"92\"}}", Encoding.UTF8, "application/json");
                request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                request.Headers.Add("authority", "parts.yamaha-motor.co.jp");
                //request.Headers.Add("content-type", "application/json");
                request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                response = await httpClient.SendAsync(request);
                answer = await response.Content.ReadAsStringAsync();
                var models = JsonConvert.DeserializeObject<Model>(answer);
                Console.WriteLine(models.);
                Console.ReadKey();

            }

           
            httpClient.Dispose();

            Console.ReadKey();
        

        }
    }
}