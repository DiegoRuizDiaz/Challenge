using ApiDevBP.Repository.Entities;
using ApiDevBP.Services.Models;
using AutoMapper;

namespace ApiDevBP.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserEntity>()
           .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserEntity, UserModel>();
            CreateMap<UserEntity, UserModelWithId>();
        }
    }
}
