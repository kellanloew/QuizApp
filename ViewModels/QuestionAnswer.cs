using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApplication.ViewModels
{
    public class QuestionAnswer
    {
        public string questionText { get; set; }
        public List<Answer> answers { get; set; }
        public int id { get; set; }
        public string score { get; set; }
    }

    public class Answer
    {
        public string answerText { get; set; }
        public int id  { get; set; }
    }
}
