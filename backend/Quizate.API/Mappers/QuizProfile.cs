using AutoMapper;
using Quizate.API.Contracts;
using Quizate.Data.Models;

namespace Quizate.API.Helpers.Mappers;

public class QuizProfile : Profile
{
    public QuizProfile()
    {
        CreateMap<Quiz, QuizResponse>()
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.Username : null))
            .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.Questions.Count))
            .ForMember(dest => dest.AttemptsCount, opt => opt.MapFrom(src => src.Attempts.Count));
    }
}
