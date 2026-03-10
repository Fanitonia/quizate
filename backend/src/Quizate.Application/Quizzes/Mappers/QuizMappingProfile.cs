using AutoMapper;
using Quizate.Application.Quizzes.DTOs.Responses;
using Quizate.Core.Entities.Quizzes;

namespace Quizate.Application.Quizzes.Mappers;

public class QuizMappingProfile : Profile
{
    public QuizMappingProfile()
    {
        CreateMap<Quiz, QuizResponse>()
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.Username : null))
            .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.Questions.Count))
            .ForMember(dest => dest.AttemptsCount, opt => opt.MapFrom(src => src.Attempts.Count));
    }
}
