using FeedbackWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackWebsite.Data
{
    public sealed class FeedbackWebsiteContext : DbContext
    {
        public FeedbackWebsiteContext (DbContextOptions<FeedbackWebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<EventInfoModel> EventInfoModel { get; set; }

        public DbSet<AnswerEnum> AnswerEnum { get; set; }

        public DbSet<AnswerText> AnswerText { get; set; }

        public DbSet<AnswersInfoModel> AnswersInfoModel { get; set; }

        public DbSet<QuestionTextModel> QuestionTextModel { get; set; }

        public DbSet<UserEventModel> UserEventModels { get; set; }

        public FeedbackWebsiteContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionTextModel>().HasData(
                new QuestionTextModel[]
                {
                    new QuestionTextModel{Id = 1, IsEnum = true, QuestionText = "1. The presenter(s) possessed appropriate competences, knowledge and expertise in the field demonstrated on the event."},
                    new QuestionTextModel{Id = 2, IsEnum = true, QuestionText = "2. The presenter(s) guided the event effectively, taking interactive approach in keeping the audience attention focused."},
                    new QuestionTextModel{Id = 3, IsEnum = true, QuestionText = "3. The event framework was easy to follow with duration optimized to cover the planned themes and discussions."},
                    new QuestionTextModel{Id = 4, IsEnum = true, QuestionText = "4. The event was conducted in affirmative manner and pleasant atmosphere."},
                    new QuestionTextModel{Id = 5, IsEnum = true, QuestionText = "5. The organizers offered consultancy and assistance outside planned timeframe and after the event ending."},
                    new QuestionTextModel{Id = 6, IsEnum = true, QuestionText = "6. The materials and content were beneficial, offering new and knowledgeable information appropriate to my skills and experience level."},
                    new QuestionTextModel{Id = 7, IsEnum = true, QuestionText = "7. I could successfully implement the event information and knowledge in my daily job activities."},
                    new QuestionTextModel{Id = 8, IsEnum = true, QuestionText = "8. The event met my expectations and I found the overall program valuable and useful."},
                    new QuestionTextModel{Id = 9, IsEnum = true, QuestionText = "9. I would recommend this event to my colleagues."},
                    new QuestionTextModel{Id = 10, IsEnum = false, QuestionText = "Comments: Please give a brief comment on the things you liked on this event, and suggest ideas for improvement of the future events of this kind."}
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
