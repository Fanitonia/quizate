using AutoMapper;
using Quizate.API.Contracts;
using Quizate.Data.Models;

namespace Quizate.API.Helpers.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserInfoResponse>();
        CreateMap<User, MyInfoResponse>();
    }
}
