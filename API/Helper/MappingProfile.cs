﻿using AutoMapper;
using DAL.DTO;
using DAL.Models;

namespace API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<ContextImage, ContextImageDTO>().ReverseMap();
            CreateMap<LanguageStat, LanguageStatDTO>().ReverseMap();
            CreateMap<GameFillBlank, GameFillBlankDTO>().ReverseMap();
            CreateMap<GameFlashCard, GameFlashCardDTO>().ReverseMap();
            CreateMap<GamePickSentence, GamePickSentenceDTO>().ReverseMap();


        }
    }
}
