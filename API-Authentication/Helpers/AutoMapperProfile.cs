using AutoMapper;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Dtosupdate;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDetails, UserDto>();
            CreateMap<UserDto, UserDetails>();
            CreateMap<UserDetails,emaildto>();
            CreateMap<emaildto,UserDetails>();
        }
    }
}