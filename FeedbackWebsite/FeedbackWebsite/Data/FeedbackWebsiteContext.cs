using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FeedbackWebsite.Models;

namespace FeedbackWebsite.Models
{
    public class FeedbackWebsiteContext : DbContext
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

        public FeedbackWebsiteContext()
        {
            Database.EnsureCreated();
        }
    }
}
