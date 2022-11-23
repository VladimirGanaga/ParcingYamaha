using Azure.Core;
using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha
{
    internal class ParcingSite 
    {

        public async Task ParceModelsAsync(HttpClient httpClient, SampleContext context)
        {


            var request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/product_list/");
            request.Content = new StringContent("{\"baseCode\":\"7306\",\"langId\":\"92\"}", Encoding.UTF8, "application/json");
            request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            

            context.Modeldatacollection.RemoveRange(context.Modeldatacollection);
            context.SaveChanges();


            var response = await httpClient.SendAsync(request);
            var answer = await response.Content.ReadAsStringAsync();
            var engineSizeListAll = JsonConvert.DeserializeObject<JsondeserializeClasses>(answer, jsonSettings);
            var engineSizeList = engineSizeListAll.displacementDataCollection.Where(x => x.productId == "10").ToList();


            foreach (var engine in engineSizeList)
            {
                Console.WriteLine($"объем: {engine.displacement}, номер: {engine.displacementType}, ИД продукта: {engine.productId}");
                request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/");
                if (int.Parse(engine.displacementType) != 0)
                {
                    request.Content = new StringContent($"{{ \"productId\":\"10\",\"displacementType\":\"{engine.displacementType}\",\"baseCode\":\"7306\",\"langId\":\"92\"}}", Encoding.UTF8, "application/json");
                    request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                    request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                    response = await httpClient.SendAsync(request);
                    answer = await response.Content.ReadAsStringAsync();
                }
                var models = JsonConvert.DeserializeObject<Model>(answer, jsonSettings);
                foreach (var bikeModel in models.modelNameDataCollection)
                {
                    Console.WriteLine(bikeModel.modelName);
                    request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/");
                    request.Content = new StringContent($"{{\"productId\":\"10\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}}", Encoding.UTF8, "application/json");
                    request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                    request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                    response = await httpClient.SendAsync(request);
                    answer = await response.Content.ReadAsStringAsync();
                    var yearModels = JsonConvert.DeserializeObject<Modelyeardata>(answer, jsonSettings);
                    foreach (var yearModel in yearModels.modelYearDataCollection)
                    {
                        Console.WriteLine($"год: {yearModel.modelYear}, id: {yearModel.productId}");

                        request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/");
                        request.Content = new StringContent($"{{ \"productId\":\"10\",\"calledCode\":\"1\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"modelYear\":\"{yearModel.modelYear}\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}}", Encoding.UTF8, "application/json");
                        request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                        request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                        response = await httpClient.SendAsync(request);
                        answer = await response.Content.ReadAsStringAsync();
                        var modelsList = JsonConvert.DeserializeObject<Modeldatacollections>(answer, jsonSettings);
                        foreach (var model in modelsList.modelDataCollection)
                        {
                            Console.WriteLine($"год: {model.modelBaseCode}, productNo: {model.productNo}, calledCode: {model.calledCode}, modelName: {model.modelName}, colorName: {model.colorName}, modelName: {model.modelName}");


                            context.Modeldatacollection.Add(model);


                        }

                    }
                }
                context.SaveChanges();



            }
            httpClient.Dispose();
        }

    }
}
