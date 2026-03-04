using System;
using System.Collections.Generic;
using System.Text;

namespace Quizate.Data.Models;

public class RefreshToken
{
    //TODO: ID'ye gerek var mı? TokenHash'i pk yapabiliriz.
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TokenHash { get; set; } = default!;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
