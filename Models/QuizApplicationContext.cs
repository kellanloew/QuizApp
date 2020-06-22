using Microsoft.EntityFrameworkCore;

namespace QuizApplication.Models
{
    public class QuizApplicationContext : DbContext
    {
        public QuizApplicationContext(DbContextOptions<QuizApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-HNPMQTA\ckc;Database=test;User Id=sa;password=pass",
                options => options.EnableRetryOnFailure()
            );
        }

        public DbSet<QuestionItem> QuestionItems { get; set; }
    }
}