using AutoMapper;
using WebApi.GoogleDtonamespace;
using WebApi.GoogleEntities;


namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDetails, GoogleDto>();
            CreateMap<GoogleDto,UserDetails>();
        }
    }
}