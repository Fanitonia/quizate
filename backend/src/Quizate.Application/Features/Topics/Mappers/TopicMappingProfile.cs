using AutoMapper;
using Quizate.Application.Features.Topics.DTOs.Requests;
using Quizate.Domain.Entities.Quizzes;

namespace Quizate.Application.Features.Topics.Mappers;

internal class TopicMappingProfile : Profile
{
    public TopicMappingProfile()
    {
        CreateMap<CreateTopicRequest, QuizTopic>();
    }
}
