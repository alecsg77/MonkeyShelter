using AutoMapper;
using MonkeyShelter.App.Model;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Web.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MonkeyDetails, MonkeyDto>();
            CreateMap<MonkeyRegistry, MonkeyIndexDto>();

            CreateMap<PutMonkeyDto, UpdateRegistryRequest>();
            CreateMap<PostMonkeyDto, RegisterMonkeyRequest>();
        }
    }
}
