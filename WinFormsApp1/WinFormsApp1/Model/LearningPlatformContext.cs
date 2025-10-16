using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.Model;

public partial class LearningPlatformContext : DbContext
{
    public LearningPlatformContext()
    {
    }

    public LearningPlatformContext(DbContextOptions<LearningPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttemptAnswer> AttemptAnswers { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassAnnouncement> ClassAnnouncements { get; set; }

    public virtual DbSet<ClassAssignment> ClassAssignments { get; set; }

    public virtual DbSet<ClassStudent> ClassStudents { get; set; }

    public virtual DbSet<ContentShare> ContentShares { get; set; }

    public virtual DbSet<ContentTag> ContentTags { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseContent> CourseContents { get; set; }

    public virtual DbSet<CoursePurchase> CoursePurchases { get; set; }

    public virtual DbSet<CourseSection> CourseSections { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<FlashcardPracticeLog> FlashcardPracticeLogs { get; set; }

    public virtual DbSet<FlashcardSet> FlashcardSets { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationChannel> NotificationChannels { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionClozeBlank> QuestionClozeBlanks { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<QuestionRangeAnswer> QuestionRangeAnswers { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SavedItem> SavedItems { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<SubmissionItem> SubmissionItems { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestAssignment> TestAssignments { get; set; }

    public virtual DbSet<TestAttempt> TestAttempts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserSetting> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttemptAnswer>(entity =>
        {
            entity.Property(e => e.Score).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Attempt).WithMany(p => p.AttemptAnswers)
                .HasForeignKey(d => d.AttemptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AAnswers_Attempt");

            entity.HasOne(d => d.Grader).WithMany(p => p.AttemptAnswers)
                .HasForeignKey(d => d.GraderId)
                .HasConstraintName("FK_AAnswers_Grader");

            entity.HasOne(d => d.Question).WithMany(p => p.AttemptAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AAnswers_Question");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId);

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.EntityType).HasMaxLength(50);
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertId);

            entity.HasIndex(e => e.VerifyCode, "UQ_Certificates_Verify").IsUnique();

            entity.Property(e => e.IssuedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Serial).HasMaxLength(50);
            entity.Property(e => e.VerifyCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasIndex(e => e.Code, "UQ_Classes_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Term).HasMaxLength(50);
        });

        modelBuilder.Entity<ClassAnnouncement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<ClassAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId);

            entity.Property(e => e.GradingPolicy).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClassStudent>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.StudentId });

            entity.Property(e => e.JoinedAt).HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<ContentShare>(entity =>
        {
            entity.HasKey(e => e.ShareId);

            entity.Property(e => e.CanView).HasDefaultValue(true);
            entity.Property(e => e.ContentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.TargetType)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContentTag>(entity =>
        {
            entity.Property(e => e.ContentType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Tag).WithMany(p => p.ContentTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentTags_Tag");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.Slug, "UQ_Courses_Slug").IsUnique();

            entity.Property(e => e.CoverUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasDefaultValue("VND");
            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Slug).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<CourseContent>(entity =>
        {
            entity.Property(e => e.ContentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TitleOverride).HasMaxLength(200);
        });

        modelBuilder.Entity<CoursePurchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId);

            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.PricePaid).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.PurchasedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CourseSection>(entity =>
        {
            entity.HasKey(e => e.SectionId);

            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorId);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Message).HasMaxLength(4000);
            entity.Property(e => e.Severity)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.MimeType).HasMaxLength(100);
            entity.Property(e => e.StoragePath).HasMaxLength(500);

            entity.HasOne(d => d.Owner).WithMany(p => p.Files)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Files_Owner");
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasKey(e => e.CardId);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Hint).HasMaxLength(500);

            entity.HasOne(d => d.BackMedia).WithMany(p => p.FlashcardBackMedia)
                .HasForeignKey(d => d.BackMediaId)
                .HasConstraintName("FK_Flashcards_BackMedia");

            entity.HasOne(d => d.FrontMedia).WithMany(p => p.FlashcardFrontMedia)
                .HasForeignKey(d => d.FrontMediaId)
                .HasConstraintName("FK_Flashcards_FrontMedia");

            entity.HasOne(d => d.Set).WithMany(p => p.Flashcards)
                .HasForeignKey(d => d.SetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flashcards_Set");
        });

        modelBuilder.Entity<FlashcardPracticeLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.Property(e => e.EaseFactor).HasColumnType("decimal(4, 2)");

            entity.HasOne(d => d.Card).WithMany(p => p.FlashcardPracticeLogs)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FPL_Card");

            entity.HasOne(d => d.Set).WithMany(p => p.FlashcardPracticeLogs)
                .HasForeignKey(d => d.SetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FPL_Set");

            entity.HasOne(d => d.User).WithMany(p => p.FlashcardPracticeLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FPL_User");
        });

        modelBuilder.Entity<FlashcardSet>(entity =>
        {
            entity.HasKey(e => e.SetId);

            entity.Property(e => e.CoverUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Language).HasMaxLength(20);
            entity.Property(e => e.TagsText).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Visibility)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Owner).WithMany(p => p.FlashcardSets)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlashcardSets_Owner");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasKey(e => e.InviteId);

            entity.HasIndex(e => e.Token, "UQ_Invitations_Token").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.RoleSuggested)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Token).HasMaxLength(100);
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Type)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NotificationChannel>(entity =>
        {
            entity.HasKey(e => e.ChannelId);

            entity.Property(e => e.AddressOrToken).HasMaxLength(500);
            entity.Property(e => e.Channel)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Enabled).HasDefaultValue(true);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.Provider)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.ProviderRef).HasMaxLength(120);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.Points)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.StemMedia).WithMany(p => p.Questions)
                .HasForeignKey(d => d.StemMediaId)
                .HasConstraintName("FK_Questions_StemMedia");

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Test");
        });

        modelBuilder.Entity<QuestionClozeBlank>(entity =>
        {
            entity.HasKey(e => e.BlankId);

            entity.Property(e => e.AcceptRegex).HasMaxLength(400);
            entity.Property(e => e.CorrectText).HasMaxLength(400);

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionClozeBlanks)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QCloze_Question");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.OptionId);

            entity.HasOne(d => d.OptionMedia).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.OptionMediaId)
                .HasConstraintName("FK_QOptions_Media");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QOptions_Question");
        });

        modelBuilder.Entity<QuestionRangeAnswer>(entity =>
        {
            entity.HasKey(e => e.RangeId);

            entity.Property(e => e.MaxValue).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.MinValue).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.Tolerance).HasColumnType("decimal(12, 4)");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionRangeAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QRange_Question");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.Property(e => e.RelatedType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Roles_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SavedItem>(entity =>
        {
            entity.Property(e => e.AddedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ContentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(500);
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.Property(e => e.MaxScore).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.TotalScore).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<SubmissionItem>(entity =>
        {
            entity.Property(e => e.MaxScore).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.RefType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Score).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasIndex(e => e.Name, "UQ_Tags_Name").IsUnique();

            entity.HasIndex(e => e.Slug, "UQ_Tags_Slug").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(80);
            entity.Property(e => e.Slug).HasMaxLength(100);
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.GradingMode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Visibility)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Owner).WithMany(p => p.Tests)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tests_Owner");
        });

        modelBuilder.Entity<TestAssignment>(entity =>
        {
            entity.HasKey(e => e.TestAssignId);
        });

        modelBuilder.Entity<TestAttempt>(entity =>
        {
            entity.HasKey(e => e.AttemptId);

            entity.Property(e => e.MaxScore).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Score).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.StartedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.Test).WithMany(p => p.TestAttempts)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attempts_Test");

            entity.HasOne(d => d.User).WithMany(p => p.TestAttempts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attempts_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(1);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Role"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_User"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.GradeLevel).HasMaxLength(50);
            entity.Property(e => e.Locale).HasMaxLength(10);
            entity.Property(e => e.SchoolName).HasMaxLength(200);
            entity.Property(e => e.TimeZone).HasMaxLength(64);

            entity.HasOne(d => d.User).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProfiles_User");
        });

        modelBuilder.Entity<UserSetting>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.EmailOptIn).HasDefaultValue(true);
            entity.Property(e => e.Language).HasMaxLength(10);
            entity.Property(e => e.PushOptIn).HasDefaultValue(true);
            entity.Property(e => e.TimeZone).HasMaxLength(64);
            entity.Property(e => e.UiTheme)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
