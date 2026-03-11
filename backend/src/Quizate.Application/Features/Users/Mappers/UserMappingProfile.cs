using AutoMapper;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Domain.Entities.Users;

namespace Quizate.Application.Features.Users.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserInfoResponse>();
        CreateMap<User, MyInfoResponse>();
    }
}
