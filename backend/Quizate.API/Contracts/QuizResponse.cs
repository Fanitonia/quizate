namespace Quizate.API.Contracts
{
    public class QuizResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid? CreatorId { get; set; }
        public string? CreatorName { get; set; }

        public Guid QuizTypeId { get; set; }
        public required string QuizTypeName { get; set; }

        public required string LanguageCode { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public bool IsPublic { get; set; }
        public int QuestionsCount { get; set; }
        public int AttemptsCount { get; set; }
    }
}
