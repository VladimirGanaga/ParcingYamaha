using AutoMapper;
using Newtonsoft.Json;
using ParcingYamaha.Dtos;
using ParcingYamaha.Models;
using ParcingYamaha.Networks;
using System;

namespace ParcingYamaha.Services
{
    /// <summary>
    /// Класс для парсинга всех моделей мотоциклов с сайта и сохранения в БД SQL
    /// </summary>
    internal class ParcingModels
    {
        MotoContext _context;
        NetworkService postrequest;
        ModelsDB modelDB;
        private readonly IMapper _mapper;
        public ParcingModels(MotoContext context, NetworkService postrequest, ModelsDB modelDB, IMapper mapper)
        {
            _context = context;
            this.postrequest = postrequest;
            this.modelDB = modelDB;
            _mapper = mapper;
        }

        public async Task ParceModelsAsync()
        {
            int count = 0;
            _context.ModelDB.RemoveRange(_context.ModelDB);
            _context.SaveChanges();

            var stringContetn = "{\"baseCode\":\"7306\",\"langId\":\"92\"}";
            var engineSizeListAll = await postrequest.PostRequest<JsondeserializeClasses>("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/product_list/", stringContetn);
            var engineSizeList = engineSizeListAll.displacementDataCollection.Where(x => x.productId == "10").ToList();

            foreach (var engine in engineSizeList)
            {
                Console.WriteLine($"объем: {engine.displacement}, номер: {engine.displacementType}, ИД продукта: {engine.productId}");

                if (int.Parse(engine.displacementType) != 0)
                {
                    stringContetn = $"{{ \"productId\":\"10\",\"displacementType\":\"{engine.displacementType}\",\"baseCode\":\"7306\",\"langId\":\"92\"}}";
                }
                var models = await postrequest.PostRequest<Model>("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_name_list/", stringContetn);
                
                foreach (var bikeModel in models.modelNameDataCollection)
                {
                    Console.WriteLine(bikeModel.modelName);

                    stringContetn = $"{{\"productId\":\"10\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\"}}";
                    var yearModels = await postrequest.PostRequest<Modelyeardata>("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_year_list/", stringContetn);
                    foreach (var yearModel in yearModels.modelYearDataCollection)
                    {
                        Console.WriteLine($"год: {yearModel.modelYear}, id: {yearModel.productId}");

                        stringContetn = $"{{ \"productId\":\"10\",\"calledCode\":\"1\",\"modelName\":\"{bikeModel.modelName}\",\"nickname\":\"{bikeModel.nickname}\",\"modelYear\":\"{yearModel.modelYear}\",\"modelTypeCode\":null,\"productNo\":null,\"colorType\":null,\"vinNo\":null,\"prefixNoFromScreen\":null,\"serialNoFromScreen\":null,\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"destination\":\"GBR\",\"destGroupCode\":\"EURS\",\"domOvsId\":\"2\",\"useProdCategory\":true,\"greyModelSign\":false}}";
                        var modelsList = await postrequest.PostRequest<Modeldatacollections>("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/model_list/", stringContetn); 
                        foreach (var model in modelsList.modelDataCollection)
                        {
                            count++;
                            Console.WriteLine($"Счетчик: {count}. Год: {model.modelBaseCode}, productNo: {model.productNo}, calledCode: {model.calledCode}, modelName: {model.modelName}, colorName: {model.colorName}, modelName: {model.modelName}");
                            modelDB =  _mapper.Map<ModelsDB>(model);
                            _context.ModelDB.Add(modelDB);
                        }
                    }
                }
                _context.SaveChanges();
            }
        }
    }
}
