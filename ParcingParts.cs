using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcingYamaha.ClassesDB;
using System.Net.Http;

namespace ParcingYamaha
{

    /// <summary>
    /// класс для прасинга запчастей для конкретной модели и сохранения в БД SQL
    /// </summary>
    internal class ParcingParts
    {

        MotoContext _context;

        public ParcingParts(MotoContext motoContext)
        {
            _context = motoContext;
        }


        public async Task GetParts(HttpClient httpClient, string desiredModel)
        {
            
            //_context.ChapterDB.RemoveRange(_context.ChapterDB);
            //_context.PartDB.RemoveRange(_context.PartDB);
            //_context.SaveChanges();

            NetworkService postrequest = new NetworkService();

            var desiredModelsFromBase = _context.ModelDB.Where(dm => dm.modelName == desiredModel).ToList();
            if (desiredModelsFromBase.IsNullOrEmpty())
            {
                desiredModelsFromBase = _context.ModelDB.Where(dm => dm.nickname == desiredModel).ToList();
                if (desiredModelsFromBase.IsNullOrEmpty())
                {
                    var complexModel = desiredModel.Split(" - ");
                    desiredModelsFromBase = _context.ModelDB.Where(dm => dm.modelName == complexModel[1]).ToList();

                }
            }
            if (!desiredModelsFromBase.IsNullOrEmpty())
            {
                foreach (ModelsDB dm in desiredModelsFromBase)
                {
                    Console.WriteLine($"Модель {dm.modelName}, Никнэйм ({dm.nickname})");
                                       
                    var jsonSettings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                                        
                    var stringContetn = $"{{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"{dm.modelTypeCode}\",\"modelYear\":\"{dm.modelYear}\",\"productNo\":\"{dm.productNo}\",\"colorType\":\"{dm.colorType}\",\"modelName\":\"{dm.modelName}\",\"prodCategory\":\"10\",\"calledCode\":\"2\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}}";
                    var answer = await postrequest.PostRequest("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/", stringContetn, httpClient);
                    var selectedModels = JsonConvert.DeserializeObject<SelectedModel>(answer, jsonSettings);
                    foreach (var selectedModel in selectedModels.figDataCollection)
                    {
                        ChaptersDB chaptersDB = new ChaptersDB();
                        //chaptersDB.modelsDB = dm;
                        chaptersDB.ModelsDBID = dm.Id;
                        chaptersDB.partFile = selectedModel.illustFileURL;
                        chaptersDB.chapter = selectedModel.figName;
                        chaptersDB.chapterID = selectedModel.figNo;
                        _context.ChapterDB.Add(chaptersDB);
                        _context.SaveChanges();
                                               
                        stringContetn = $"{{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"{dm.modelTypeCode}\",\"modelYear\":\"{dm.modelYear}\",\"productNo\":\"{dm.productNo}\",\"colorType\":\"{dm.colorType}\",\"modelName\":\"{dm.modelName}\",\"vinNoSearch\":\"false\",\"figNo\":\"{selectedModel.figNo}\",\"figBranchNo\":\"{selectedModel.figBranchNo}\",\"catalogNo\":\"{selectedModels.catalogNo}\",\"illustNo\":\"{selectedModel.illustNo}\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"domOvsId\":\"2\",\"greyModelSign\":false,\"cataPBaseCode\":\"7451\",\"currencyCode\":\"GBP\"}}";
                        answer = await postrequest.PostRequest("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_text/", stringContetn, httpClient);
                        var partsCollection = JsonConvert.DeserializeObject<SelectParts>(answer, jsonSettings);
                        foreach (var part in partsCollection.partsDataCollection)
                        {
                            Console.WriteLine($"Chapter: {selectedModel.figName}, partNo: {part.partNo}, partName: {part.partName}");
                            PartsDB partsDB = new PartsDB();
                            //partsDB.chapterDB = chaptersDB;
                            partsDB.chapterID= chaptersDB.Id;
                            partsDB.partNo= part.partNo;
                            partsDB.partName = part.partName;
                            partsDB.quantity = part.quantity;
                            partsDB.refNo = part.refNo;
                            _context.PartDB.Update(partsDB);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }



    }
}



