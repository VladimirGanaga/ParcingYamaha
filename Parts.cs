using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha
{
    

    internal class Parts
    {
        //{"productId":"10","modelBaseCode":"","modelTypeCode":"2TV0","modelYear":"1987","productNo":"010","colorType":"A","modelName":"SDR","prodCategory":"10","calledCode":"1","vinNoSearch":"false","catalogLangId":"","baseCode":"7306","langId":"92","userGroupCode":"BTOC","greyModelSign":false}
        //{"productId":"10","modelBaseCode":"","modelTypeCode":"2TV0","modelYear":"1987","productNo":"010","colorType":"A","modelName":"SDR","prodCategory":"10","calledCode":"1","vinNoSearch":"false","catalogLangId":"01","baseCode":"7306","langId":"92","userGroupCode":"BTOC","greyModelSign":false}
        //{ "productId":"10","modelBaseCode":"","modelTypeCode":"2TV0","modelYear":"1987","productNo":"010","colorType":"A","modelName":"SDR","vinNoSearch":"false","figNo":"36","figBranchNo":"1","catalogNo":"172TV010J1","illustNo":"2TV010-7360","catalogLangId":"01","baseCode":"7306","langId":"92","userGroupCode":"BTOC","domOvsId":"2","greyModelSign":false,"cataPBaseCode":"7451","currencyCode":"GBP"}
        public async Task GetParts(HttpClient httpClient, SampleContext context)
        {
            

            var request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/");
            request.Content = new StringContent($"{{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"BW32\",\"modelYear\":\"2020\",\"productNo\":\"060\",\"colorType\":\"A\",\"modelName\":\"XTZ690 - U\",\"prodCategory\":\"10\",\"calledCode\":\"1\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"01\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}}", Encoding.UTF8, "application/json");
            request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var response = await httpClient.SendAsync(request);
            var answer = await response.Content.ReadAsStringAsync();
            var selectedModels = JsonConvert.DeserializeObject<SelectedModel>(answer, jsonSettings);
            
            foreach (var selectedModel in selectedModels.figDataCollection)
            {
                Console.WriteLine($"figName: {selectedModel.figName}, figNo: {selectedModel.figNo}, figBranchNo: {selectedModel.figBranchNo}");
                //request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/");
                //if (int.Parse(engine.displacementType) != 0)
                //{
                //    request.Content = new StringContent($"{{ \"productId\":\"10\",\"displacementType\":\"{engine.displacementType}\",\"baseCode\":\"7306\",\"langId\":\"92\"}}", Encoding.UTF8, "application/json");
                //    request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                //    request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                //    response = await httpClient.SendAsync(request);
                //    answer = await response.Content.ReadAsStringAsync();
                //}
                //var models = JsonConvert.DeserializeObject<Model>(answer, jsonSettings);
                //foreach (var bikeModel in models.modelNameDataCollection)
                //{
                //    Console.WriteLine(bikeModel.modelName);
                //    request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/");
                //    request.Content = new StringContent($"{{\"productId\":\"10\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}}", Encoding.UTF8, "application/json");
                //    request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                //    request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                //    response = await httpClient.SendAsync(request);
                //    answer = await response.Content.ReadAsStringAsync();
                //    var yearModels = JsonConvert.DeserializeObject<Modelyeardata>(answer, jsonSettings);
                //    foreach (var yearModel in yearModels.modelYearDataCollection)
                //    {
                //        Console.WriteLine($"год: {yearModel.modelYear}, id: {yearModel.productId}");

                //        request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/");
                //        request.Content = new StringContent($"{{ \"productId\":\"10\",\"calledCode\":\"1\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"modelYear\":\"{yearModel.modelYear}\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}}", Encoding.UTF8, "application/json");
                //        request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                //        request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                //        response = await httpClient.SendAsync(request);
                //        answer = await response.Content.ReadAsStringAsync();
                //        var modelsList = JsonConvert.DeserializeObject<Modeldatacollections>(answer, jsonSettings);
                //        foreach (var model in modelsList.modelDataCollection)
                //        {
                //            Console.WriteLine($"год: {model.modelBaseCode}, productNo: {model.productNo}, calledCode: {model.calledCode}, modelName: {model.modelName}, colorName: {model.colorName}, modelName: {model.modelName}");


                //            context.Modeldatacollection.Update(model);



                //        }

                //    }
                //}
                //context.SaveChanges();



            }
            
        }

    }
}



