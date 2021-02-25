using AutoMapper;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Web.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Monkey, MonkeyDto>();
            CreateMap<PutMonkeyDto, Monkey>();
            CreateMap<PostMonkeyDto, Monkey>();
        }
    }
}
