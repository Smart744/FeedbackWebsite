using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FeedbackWebsite.Utils;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FeedbackWebsite.Models
{
    public enum Answer
    {
        [Display(Name = "Strongly Disagree")]
        StronglyDisagree,
        [Display(Name = "Disagree")]
        Disagree,
        [Display(Name = "Neither Agree nor Disagree")]
        NeitherAgreeNorDisagree,
        [Display(Name = "Agree")]
        Agree,
        [Display(Name = "Strongly Agree")]
        StronglyAgree
    }

    public class AnswersInfoModel
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int QuestionId { get; set; }
    }

    public class AnswerEnum : AnswersInfoModel
    {
        public Answer QuestionEnumAnswer { get; set; }
    }

    public class AnswerText : AnswersInfoModel
    {
        public string AnswerTextAnswer { get; set; }
    }
}
