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

        public DbSet<FeedbackWebsite.Models.EventInfoModel> EventInfoModel { get; set; }

        public DbSet<FeedbackWebsite.Models.QuestionsInfoModel> QuestionsInfoModel { get; set; }
    }
}
