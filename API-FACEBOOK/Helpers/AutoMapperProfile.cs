using AutoMapper;
using WebApi.Dtos;
using WebApi.FacebookEntities;
using WebApi.FacebookDatatransfer;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDetails, FacebookDto>();
            CreateMap<FacebookDto,UserDetails>();
        }
    }
}