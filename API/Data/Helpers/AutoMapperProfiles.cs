using AutoMapper;
using DatingApp.API.Data.Core.Extensions;
using DatingApp.API.DTOs;
using DatingApp.API.Entities;

namespace DatingApp.API.Data.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MemberDto, AppUser>().ReverseMap()
                .ForMember(dest => dest.PhotoUrl, opt => opt
                .MapFrom(src => src.Photos!.FirstOrDefault(a => a.IsMain)!.URL))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>().ReverseMap();
        }
    }
}
