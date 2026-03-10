using AutoMapper;
using Quizate.Application.Users.DTOs.Responses;
using Quizate.Core.Entities.Users;

namespace Quizate.Application.Users.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserInfoResponse>();
        CreateMap<User, MyInfoResponse>();
    }
}
