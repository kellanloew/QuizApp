using System;
using System.Collections.Generic;
using System.Linq;
using QuizApplication.Web.Models;

namespace QuizApplication.Web.ViewModels
{
    public class QuestionAnswer
    {
        public string questionText { get; set; }
        public IList<AnswerDisplay> answers { get; set; }
        public int id { get; set; }
        public string score { get; set; }
        public bool isError { get; set; }
        public int numberWrong { get; set; }

        //intializes the next question object, whether there is a question left or just the score
        public void ShowNextQuestionOrScore(quiz_dbContext _context)
        {
            using (_context)
            {
                //get first question that has not been answered
                Question Question = _context.Questions.FirstOrDefault(q => q.IsComplete == false);
                //if the question exists, then fetch the corresponding answers
                if (Question != null)
                {
                    questionText = Question.QuestionText;
                    id = Question.Id;

                    //get answers associated with question
                    SetAnswerOptionsForQuestion(_context);
                    shuffleAnswers();
                }
                //if there is no answered question, the quiz is finished, so calculate the user score
                else
                {
                    //get all user selected answers options

                    var dbAnswers = (from c in _context.Answers select c).Where(p => p.WasSelected == true).ToList();
                    int questionCount = _context.Questions.Count(); //get number of questions answered

                    //calculate the percentage
                    CalculatePercentage(dbAnswers, questionCount);
                }
            }
            //return currentQuestion;
        }

        //gets the answer options for a given question id
        private void SetAnswerOptionsForQuestion(quiz_dbContext _context)
        {
            //initialize list of answer options
            var dbAnswers = new List<Answer>();
            //get the answer options for this question from the DB
            dbAnswers = (from c in _context.Answers select c).Where(p => p.QuestionId == id).ToList();

            //throw an exception if no answers were found
            if (dbAnswers.Count == 0)
            {
                throw new Exception("No answer options could be found for question id " + id);
            }

            answers = new List<AnswerDisplay>();
            
            foreach (Answer a in dbAnswers)
            {
                //define the properties of each viewmodel
                AnswerDisplay currentAnswer = new AnswerDisplay();
                currentAnswer.answerText = a.AnswerText;
                currentAnswer.id = a.Id;
                answers.Add(currentAnswer);
            }
        }

        //calculate user's score based on right and wrong answers chosen
        public void CalculatePercentage(List<Answer> dbAnswers, int numQuestions)
        {
            int correct = 0;
            if(numQuestions > 0)
            {
                foreach (Answer a in dbAnswers)
                {
                    if (a.IsCorrect) correct += 1;
                }
                score = ((correct * 100) / numQuestions).ToString();
                numberWrong = numQuestions - correct;
            }
            else
            {
                score = "0";
                numberWrong = 0;
            }
        }

        //Randomly shuffles answer options
        internal void shuffleAnswers()
        {
            Random rng = new Random();
            int n = answers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = answers[k];
                answers[k] = answers[n];
                answers[n] = value;
            }
        }

    }
}
