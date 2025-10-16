using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string? Phone { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<FlashcardPracticeLog> FlashcardPracticeLogs { get; set; } = new List<FlashcardPracticeLog>();

    public virtual ICollection<FlashcardSet> FlashcardSets { get; set; } = new List<FlashcardSet>();

    public virtual ICollection<TestAttempt> TestAttempts { get; set; } = new List<TestAttempt>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    public virtual UserProfile? UserProfile { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
