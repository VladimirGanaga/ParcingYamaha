using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using ParcingYamaha.Dtos;
using ParcingYamaha.Models;
using ParcingYamaha.Networks;
using ParcingYamaha.Services;

namespace ParcingYamaha.Services
{

    /// <summary>
    /// класс для прасинга запчастей для конкретной модели и сохранения в БД SQL
    /// </summary>
    internal class ParcingPartsService : IParcingParts
    {
        MotoContext _context;
        NetworkService postrequest;
        ChaptersDB chaptersDB;
        PartsDB partsDB;
        private readonly IMapper _mapper;
        public ParcingPartsService(MotoContext motoContext, NetworkService postrequest, ChaptersDB chaptersDB, PartsDB partsDB, IMapper mapper)
        {
            _context = motoContext;
            this.postrequest = postrequest;
            this.chaptersDB = chaptersDB;
            this.partsDB = partsDB;
            _mapper = mapper;
        }

        public async Task GetParts(string desiredModel)
        {
            _context.ChapterDB.RemoveRange(_context.ChapterDB);
            _context.PartDB.RemoveRange(_context.PartDB);
            _context.SaveChanges();

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

                    var stringContetn = $"{{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"{dm.modelTypeCode}\",\"modelYear\":\"{dm.modelYear}\",\"productNo\":\"{dm.productNo}\",\"colorType\":\"{dm.colorType}\",\"modelName\":\"{dm.modelName}\",\"prodCategory\":\"10\",\"calledCode\":\"2\",\"vinNoSearch\":\"false\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"greyModelSign\":false}}";
                    var selectedModels = await postrequest.PostRequest<SelectedModel>("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_index/", stringContetn);
                    foreach (var selectedModel in selectedModels.figDataCollection)
                    {
                        chaptersDB = _mapper.Map<ChaptersDB>(selectedModel);
                        chaptersDB.ModelsDBID = dm.Id;
                        _context.ChapterDB.Add(chaptersDB);
                        _context.SaveChanges();
                        stringContetn = $"{{\"productId\":\"10\",\"modelBaseCode\":\"\",\"modelTypeCode\":\"{dm.modelTypeCode}\",\"modelYear\":\"{dm.modelYear}\",\"productNo\":\"{dm.productNo}\",\"colorType\":\"{dm.colorType}\",\"modelName\":\"{dm.modelName}\",\"vinNoSearch\":\"false\",\"figNo\":\"{selectedModel.figNo}\",\"figBranchNo\":\"{selectedModel.figBranchNo}\",\"catalogNo\":\"{selectedModels.catalogNo}\",\"illustNo\":\"{selectedModel.illustNo}\",\"catalogLangId\":\"02\",\"baseCode\":\"7306\",\"langId\":\"92\",\"userGroupCode\":\"BTOC\",\"domOvsId\":\"2\",\"greyModelSign\":false,\"cataPBaseCode\":\"7451\",\"currencyCode\":\"GBP\"}}";
                        var partsCollection = await postrequest.PostRequest<SelectParts>("https://parts.yamaha-motor.co.jp/ypec_b2c/services/html5/catalog_text/", stringContetn);
                        foreach (var part in partsCollection.partsDataCollection)
                        {
                            Console.WriteLine($"Chapter: {selectedModel.figName}, partNo: {part.partNo}, partName: {part.partName}");
                            partsDB = _mapper.Map<PartsDB>(part);
                            partsDB.chapterID = chaptersDB.Id;
                            _context.PartDB.Add(partsDB);
                            _context.SaveChanges();
                            
                        }
                        
                    }
                }
            }
        }
    }
}

internal class ParcingParts1 : IParcingParts
{
    MotoContext _context;
    HttpClient httpClient;
    public ParcingParts1(MotoContext motoContext, HttpClient httpClient)
    {
        _context = motoContext;
        this.httpClient = httpClient;
    }

    public async Task GetParts(string desiredModel)
    {
        Console.WriteLine(11123);

    }


}


