using System;
using System.Collections.Generic;
using System.Text;

namespace Quizate.Data.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }

        public User? User { get; set; }
        public Guid UserId { get; set; }

        public Quiz? Quiz { get; set; }
        public Guid QuizId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Score { get; set; }
    }
}
