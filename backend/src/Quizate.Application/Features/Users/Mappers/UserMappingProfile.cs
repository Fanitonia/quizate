using AutoMapper;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Domain.Entities.Users;

namespace Quizate.Application.Features.Users.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserInfoResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        CreateMap<User, DetailedUserInfoResponse>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
