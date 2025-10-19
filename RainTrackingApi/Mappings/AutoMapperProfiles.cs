using AutoMapper;
using RainTrackingApi.Models.Domain;
using RainTrackingApi.Models.DTO;

namespace RainTrackingApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRainLog, RainLogResponseDto>()
                .ForMember(d => d.UserIdentifier, o => o.MapFrom(s => s.User.UserIdentifier))
                .ReverseMap();

            CreateMap<UserRainLog, CreateUserRainLogModel>()
                .ForMember(d => d.UserIdentifier, o => o.MapFrom(s => s.User.UserIdentifier))
                .ReverseMap();

            CreateMap<CreateUserRainLogModel, AddRainLogRequestDto>()
                .ReverseMap();
        }
    }
}
