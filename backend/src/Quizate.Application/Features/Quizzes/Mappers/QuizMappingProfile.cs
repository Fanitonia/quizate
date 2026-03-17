using AutoMapper;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Application.Features.Quizzes.Mappers;

public class QuizMappingProfile : Profile
{
    public QuizMappingProfile()
    {
        CreateMap<Quiz, QuizResponse>()
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.Username : null))
            .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.Questions.Count))
            .ForMember(dest => dest.AttemptsCount, opt => opt.MapFrom(src => src.Attempts.Count));

        CreateMap<CreateQuizRequest, Quiz>()
            .ForMember(dest => dest.Topics, opt => opt.Ignore())
            .ForMember(dest => dest.Questions, opt => opt.Ignore());
    }
}
