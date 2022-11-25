﻿using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha
{

    /// <summary>
    /// класс для прасинга запчастей для конкретной модели и сохранения в БД SQL
    /// </summary>
    internal class Parts
    {
        public async Task GetParts(HttpClient httpClient, SampleContext context, string desiredModel)
        {
            context.Partsdatacollection.RemoveRange(context.Partsdatacollection);
            context.SaveChanges();
            SampleContext ctx = new SampleContext();
            var desiredModelsFromBase = ctx.Modeldatacollection.Where(dm => dm.modelName == desiredModel);
            if (desiredModelsFromBase.IsNullOrEmpty())
            {
                desiredModelsFromBase = ctx.Modeldatacollection.Where(dm => dm.nickname == desiredModel);
                if (desiredModelsFromBase.IsNullOrEmpty())
                {
                    var complexModel=desiredModel.Split(" - ");
                    desiredModelsFromBase = ctx.Modeldatacollection.Where(dm => dm.modelName == complexModel[1]);
                    if (!desiredModelsFromBase.IsNullOrEmpty())
                    {
                        foreach (Modeldatacollection dm in desiredModelsFromBase)
                        {
                            Console.WriteLine($"Модель {dm.modelName}, Никнэйм ({dm.nickname})");


                            var request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/");
                            request.Content = new StringContent($"{{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"{dm.modelTypeCode}\",\"modelYear\":\"{dm.modelYear}\",\"productNo\":\"{dm.productNo}\",\"colorType\":\"{dm.colorType}\",\"modelName\":\"{dm.modelName}\",\"prodCategory\":\"10\",\"calledCode\":\"2\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}}", Encoding.UTF8, "application/json");


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

                                //Console.WriteLine($"figName: {selectedModel.figName}, figNo: {selectedModel.figNo}, figBranchNo: {selectedModel.figBranchNo}");
                                request = new HttpRequestMessage(HttpMethod.Post, "https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_text/");

                                request.Content = new StringContent($"{{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"{dm.modelTypeCode}\",\"modelYear\":\"{dm.modelYear}\",\"productNo\":\"{dm.productNo}\",\"colorType\":\"{dm.colorType}\",\"modelName\":\"{dm.modelName}\",\"vinNoSearch\":\"false\",\"figNo\":\"{selectedModel.figNo}\",\"figBranchNo\":\"{selectedModel.figBranchNo}\",\"catalogNo\":\"{selectedModels.catalogNo}\",\"illustNo\":\"{selectedModel.illustNo}\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"domOvsId\":\"2\",\"greyModelSign\":false,\"cataPBaseCode\":\"7451\",\"currencyCode\":\"GBP\"}}", Encoding.UTF8, "application/json");
                                request.Headers.Add("accept", "application/json, text/javascript, */*; q=0.01");
                                request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 YaBrowser/22.11.0.2419 Yowser/2.5 Safari/537.36");
                                response = await httpClient.SendAsync(request);
                                answer = await response.Content.ReadAsStringAsync();
                                var partsCollection = JsonConvert.DeserializeObject<SelectParts>(answer, jsonSettings);
                                foreach (var part in partsCollection.partsDataCollection)
                                {
                                    Console.WriteLine($"Chapter: {selectedModel.figName}, partNo: {part.partNo}, partName: {part.partName}");
                                    //part.modeldatacollection = dm;
                                    part.modeldatacollectionID = dm.Id;
                                    part.chapter = selectedModel.figName;
                                    context.Partsdatacollection.Update(part);
                                    context.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}


