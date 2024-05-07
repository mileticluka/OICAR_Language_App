using AdminPanel.Models;
using AutoMapper;
using DAL.DTO;
using DAL.Models;

namespace API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<GameFillBlank,GameFillBlankDTO>().ReverseMap();
        }
    }
}
