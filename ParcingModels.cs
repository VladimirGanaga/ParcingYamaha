using Azure.Core;
using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ParcingYamaha.Dtos;
using ParcingYamaha.Models;
using ParcingYamaha.Networks;

namespace ParcingYamaha
{
    /// <summary>
    /// Класс для парсинга всех моделей мотоциклов с сайта и сохранения в БД SQL
    /// </summary>
    internal class ParcingModels 
    {
        MotoContext _context;
        NetworkService postrequest;
        public ParcingModels(MotoContext context, NetworkService postrequest)
        {
            _context = context;
            this.postrequest = postrequest;
        }

        public async Task ParceModelsAsync()
        {
            int count=0;
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            _context.ModelDB.RemoveRange(_context.ModelDB);
            _context.SaveChanges();

            var stringContetn = "{\"baseCode\":\"7306\",\"langId\":\"92\"}";
            var answer = await postrequest.PostRequest("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/product_list/", stringContetn);
            var engineSizeListAll = JsonConvert.DeserializeObject<JsondeserializeClasses>( answer, jsonSettings);
            var engineSizeList = engineSizeListAll.displacementDataCollection.Where(x => x.productId == "10").ToList();

            foreach (var engine in engineSizeList)
            {
                Console.WriteLine($"объем: {engine.displacement}, номер: {engine.displacementType}, ИД продукта: {engine.productId}");
               
                if (int.Parse(engine.displacementType) != 0)
                {
                    stringContetn = $"{{ \"productId\":\"10\",\"displacementType\":\"{engine.displacementType}\",\"baseCode\":\"7306\",\"langId\":\"92\"}}";
                    answer = await postrequest.PostRequest("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/", stringContetn); 
                }
                var models = JsonConvert.DeserializeObject<Model>(answer, jsonSettings);
                foreach (var bikeModel in models.modelNameDataCollection)
                {
                    Console.WriteLine(bikeModel.modelName);
                    
                    stringContetn = $"{{\"productId\":\"10\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}}";
                    answer = await postrequest.PostRequest("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/", stringContetn);
                    var yearModels = JsonConvert.DeserializeObject<Modelyeardata>(answer, jsonSettings);
                    foreach (var yearModel in yearModels.modelYearDataCollection)
                    {
                        Console.WriteLine($"год: {yearModel.modelYear}, id: {yearModel.productId}");

                        stringContetn = $"{{ \"productId\":\"10\",\"calledCode\":\"1\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"modelYear\":\"{yearModel.modelYear}\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}}";
                        answer = await postrequest.PostRequest("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/", stringContetn);
                        var modelsList = JsonConvert.DeserializeObject<Modeldatacollections>(answer, jsonSettings);
                        foreach (var model in modelsList.modelDataCollection)
                        {
                            count++;
                            Console.WriteLine($"Счетчик: {count}. Год: {model.modelBaseCode}, productNo: {model.productNo}, calledCode: {model.calledCode}, modelName: {model.modelName}, colorName: {model.colorName}, modelName: {model.modelName}");
                            ModelsDB modelDB = new ModelsDB();
                            modelDB.modelName=model.modelName;
                            modelDB.nickname = model.nickname;
                            modelDB.modelYear = model.modelYear;
                            modelDB.modelTypeCode = model.modelTypeCode;
                            modelDB.colorType = model.colorType;
                            modelDB.colorName = model.colorName;
                            modelDB.prodPictureFileURL = model.prodPictureFileURL;
                            modelDB.productNo = model.productNo;
                            _context.ModelDB.Add(modelDB);

                        }

                    }
                }
                _context.SaveChanges();
            }
            
        }

    }
}
