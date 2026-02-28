using AutoMapper;
using Quizate.API.Contracts;
using Quizate.Data.Models;

namespace Quizate.API.Profiles
{
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizResponse>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.Username : null))
                .ForMember(dest => dest.QuizTypeName, opt => opt.MapFrom(src => src.QuizType != null ? src.QuizType.DisplayName : string.Empty))
                .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.MultipleChoiceQuestions != null ? src.MultipleChoiceQuestions.Count : 0))
                .ForMember(dest => dest.AttemptsCount, opt => opt.MapFrom(src => src.QuizAttempts != null ? src.QuizAttempts.Count : 0));
        }
    }
}
