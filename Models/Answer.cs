using System;
using System.Collections.Generic;

namespace QuizApplication.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public bool? WasSelected { get; set; }

        public virtual Question Question { get; set; }
    }
}
