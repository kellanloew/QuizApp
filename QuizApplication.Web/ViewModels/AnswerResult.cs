using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApplication.Web.ViewModels
{
    public class AnswerResult
    {
        public string optionId { get; set; }
        public string questionId { get; set; }
        public string answerText { get; set; }
    }
}
