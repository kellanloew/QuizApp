using QuizApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApplication.ViewModels
{
    public class QuestionAnswer
    {
        public string questionText { get; set; }
        public List<AnswerDisplay> answers { get; set; }
        public int id { get; set; }
        public string score { get; set; }
        public bool isError { get; set; }

        public void calculatePercentage(List<Models.Answer> dbAnswers, int numQuestions)
        {
            int correct = 0;
            foreach (Answer a in dbAnswers)
            {
                if (a.IsCorrect) correct += 1;
            }
            score = ((correct * 100) / numQuestions).ToString();
        }
    }
}
