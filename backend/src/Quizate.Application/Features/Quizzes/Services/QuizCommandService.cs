using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Common.Errors;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Languages.Errors;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Helpers;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Application.Features.Topics.Errors;
using Quizate.Domain.Entities.Questions;
using Quizate.Domain.Entities.Quizzes;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class QuizCommandService(
    QuizateDbContext context,
    IMapper mapper) : IQuizCommandService
{
    // TODO: quiz validation with FluentValidation
    public async Task<Result<QuizResponse?>> CreateQuizAsync(CreateQuizRequest request, Guid userId = default)
    {
        // topic validation
        var normalizedRequestTopics = request.Topics
            .Select(topic => topic.Trim().ToLowerInvariant())
            .Distinct()
            .ToArray();

        var topics = await context.QuizTopics
            .Where(topic => normalizedRequestTopics.Contains(topic.Name))
            .ToListAsync();

        var missingTopics = normalizedRequestTopics
            .Except(topics.Select(topic => topic.Name))
            .ToArray();

        if (missingTopics.Length > 0)
            return TopicErrors.TopicsNotFound(missingTopics);

        // language validation
        var language = await context.QuizLanguages
            .FirstOrDefaultAsync(lang => lang.Code == request.LanguageCode);
        if (language == null)
            return LanguageErrors.LanguageNotFound(request.LanguageCode);

        var quiz = mapper.Map<Quiz>(request);
        quiz.Topics = topics;

        List<Question> questions = request.Questions
        .Select(question => new Question(
            quiz.Id,
            question.QuestionType,
            QuestionPayloadSerializer.SerializeQuestionObject(question.QuestionType, question)))
        .ToList();

        quiz.Questions = questions;

        if (userId != default)
            quiz.SetCreatorId(userId);

        context.Quizzes.Add(quiz);
        await context.SaveChangesAsync();

        var quizResponse = mapper.Map<QuizResponse>(quiz);
        return Result<QuizResponse?>.Success(quizResponse);
    }

    public async Task<Result> DeleteQuizAsync(Guid quizId)
    {
        var quiz = await context.Quizzes.FindAsync(quizId);

        if (quiz == null)
            return CommonErrors.NotFound;

        context.Quizzes.Remove(quiz);
        await context.SaveChangesAsync();

        return Result.Success();
    }

    // TODO: topic updateleme
    public async Task<Result> UpdateQuizAsync(UpdateQuizRequest request, Guid quizId)
    {
        var quiz = await context.Quizzes.FindAsync(quizId);

        if (quiz == null)
            return CommonErrors.NotFound;

        quiz.UpdateTitle(request.Title);
        quiz.UpdateDescription(request.Description);
        quiz.UpdateVisibiltity(request.IsPublic);
        quiz.UpdateLanguage(request.LanguageCode);

        var updatedRows = await context.SaveChangesAsync();

        if (updatedRows == 0)
            return CommonErrors.UpdateRequestEmpty;

        return Result.Success();
    }
}