using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Common.Errors;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Quizzes.DTOs.Requests;
using Quizate.Application.Features.Quizzes.DTOs.Responses;
using Quizate.Application.Features.Quizzes.Errors;
using Quizate.Application.Features.Quizzes.Helpers;
using Quizate.Application.Features.Quizzes.Interfaces;
using Quizate.Domain.Entities.Questions;
using Quizate.Domain.Entities.Quizzes;
using Quizate.Persistence;

namespace Quizate.Application.Features.Quizzes.Services;

public class QuizCommandService(
    QuizateDbContext context,
    IMapper mapper) : IQuizCommandService
{
    public async Task<Result<QuizResponse?>> CreateQuizAsync(CreateQuizRequest request)
    {
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
            return QuizErrors.InvalidTopics(missingTopics);

        var quiz = mapper.Map<Quiz>(request);
        quiz.Topics = topics;

        context.Quizzes.Add(quiz);
        await context.SaveChangesAsync();

        List<Question> questions = request.Questions
            .Select(question => new Question(
                quiz.Id,
                question.QuestionType,
                QuestionPayloadSerializer.SerializeQuestionObject(question.QuestionType, question)))
            .ToList();

        if (questions.Count > 0)
        {
            context.Questions.AddRange(questions);
            await context.SaveChangesAsync();
        }

        var quizResponse = mapper.Map<QuizResponse>(quiz);
        return Result<QuizResponse?>.Success(quizResponse);
    }

    public async Task<Result> DeleteQuizAsync(Guid quizId)
    {
        var quiz = await context.Quizzes.FindAsync(quizId);

        if (quiz == null)
            return CommonErrors.NotFound;

        context.Quizzes.Remove(quiz);
        context.SaveChanges();

        return Result.Success();
    }

    public async Task<Result> UpdateQuizAsync(UpdateQuizRequest request, Guid quizId)
    {
        var quiz = await context.Quizzes.FindAsync(quizId);

        if (quiz == null)
            return CommonErrors.NotFound;

        quiz.UpdateTitle(request.Title);
        quiz.UpdateDescription(request.Description);
        quiz.UpdateThumbnailUrl(request.ThumbnailUrl);
        quiz.UpdateVisibiltity(request.IsPublic);
        quiz.UpdateLanguage(request.LanguageCode);

        context.Quizzes.Update(quiz);
        var updatedRows = await context.SaveChangesAsync();

        if (updatedRows == 0)
            return CommonErrors.NoChangesWereMade;

        return Result.Success();
    }
}