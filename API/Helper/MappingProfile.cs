using AutoMapper;
using DAL.DTO;
using DAL.Models;

namespace API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<GameFillBlank, GameFillBlankDTO>().ReverseMap();
            CreateMap<GameFlashCard, GameFlashCardDTO>().ReverseMap();
            CreateMap<GamePickSentence, GamePickSentence>().ReverseMap();
        }
    }
}
