using AutoMapper;
using ParcingYamaha.Dtos;
using ParcingYamaha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha.Services
{
    internal class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Modeldatacollection, ModelsDB>();
            CreateMap<Partsdatacollection, PartsDB>();
            CreateMap<Figdatacollection, ChaptersDB>()
                .ForMember(dest => dest.partFile, opt => opt.MapFrom(src => src.illustFileURL))
                .ForMember(dest => dest.chapter, opt => opt.MapFrom(src => src.figName))
                .ForMember(dest => dest.chapterID, opt => opt.MapFrom(src => src.figNo));
           

        }


    }
}
