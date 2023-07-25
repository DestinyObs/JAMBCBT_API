using Microsoft.EntityFrameworkCore;
using JAMBAPI.Models;

namespace JAMBAPI.Data
{
    public class JambDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<OLevelGrade> OLevelGrades { get; set; }
        public DbSet<IrisScan> IrisScans { get; set; }

        public JambDbContext(DbContextOptions<JambDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leaderboard>()
                .HasOne(l => l.Student)
                .WithMany()
                .HasForeignKey(l => l.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuizQuestion>()
              .HasOne(qq => qq.Quiz)
              .WithMany(q => q.QuizQuestions)
              .HasForeignKey(qq => qq.QuizId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProgress>()
                .HasOne(up => up.Student)
                .WithMany(u => u.UserProgresses)
                .HasForeignKey(up => up.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
        // Additional configuration and overrides can be added here
    }
}
