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
            .ForMember(dest => dest.QuestionCount, opt => opt.MapFrom(src => src.Questions.Count))
            .ForMember(dest => dest.AttemptCount, opt => opt.MapFrom(src => src.Attempts.Count))
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics.Select(t => t.Name).ToArray()));

        CreateMap<CreateQuizRequest, Quiz>()
            .ForMember(dest => dest.Topics, opt => opt.Ignore())
            .ForMember(dest => dest.Questions, opt => opt.Ignore());
    }
}
