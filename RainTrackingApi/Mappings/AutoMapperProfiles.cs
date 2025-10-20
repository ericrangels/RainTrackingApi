using AutoMapper;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Models.DTO;
using RainTrackingApi.Models.Request;
using RainTrackingApi.Models.Response;

namespace RainTrackingApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRainLog, RainLogResponse>()
                .ForMember(d => d.UserIdentifier, o => o.MapFrom(s => s.User.UserIdentifier))
                .ReverseMap();

            CreateMap<UserRainLog, CreateUserRainLogDto>()
                .ForMember(d => d.UserIdentifier, o => o.MapFrom(s => s.User.UserIdentifier))
                .ReverseMap();

            CreateMap<CreateUserRainLogDto, AddRainLogRequest>()
                .ReverseMap();
        }
    }
}
