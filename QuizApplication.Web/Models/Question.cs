using System;
using System.Collections.Generic;

namespace QuizApplication.Web.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsComplete { get; set; }

        public virtual ICollection<Answer> Answer { get; set; }
    }
}
