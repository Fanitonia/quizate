using AutoMapper;
using Quizate.Application.Features.Topics.DTOs.Requests;
using Quizate.Application.Features.Topics.DTOs.Responses;
using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Application.Features.Topics.Mappers;

internal class TopicMappingProfile : Profile
{
    public TopicMappingProfile()
    {
        CreateMap<QuizTopic, TopicResponse>();
        CreateMap<CreateTopicRequest, QuizTopic>();
    }
}
