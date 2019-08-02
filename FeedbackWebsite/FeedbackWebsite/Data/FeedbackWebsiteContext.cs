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
    }
}
