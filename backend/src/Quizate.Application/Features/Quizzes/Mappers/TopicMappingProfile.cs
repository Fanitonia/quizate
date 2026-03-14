using AutoMapper;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Application.Features.Quizzes.Mappers;

internal class TopicMappingProfile : Profile
{
    public TopicMappingProfile()
    {
        CreateMap<QuizTopic, TopicResponse>();
        CreateMap<CreateTopicRequest, QuizTopic>();
    }
}
