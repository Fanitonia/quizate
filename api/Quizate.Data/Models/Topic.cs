using System;
using System.Collections.Generic;
using System.Text;

namespace Quizate.Data.Models
{
    public class Topic
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public required string Name { get; set; }
        public required string DisplayName { get; set; }
        public string? Description { get; set; }

        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
