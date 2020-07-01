using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizApplication.Web.Models;
using QuizApplication.Web.ViewModels;

namespace QuizApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly quiz_dbContext _context;

        public HomeController(quiz_dbContext context)
        {
            //make the DB context available for all methods of HomeController
            _context = context;
        }

        [HttpGet]
        //Is called on page load. Get the next unanswered question from DB or displays user's score if all answered
        public ActionResult Index()
        {
            //initialize the viewmodel object that will be returned to the razor view
            QuestionAnswer question = new QuestionAnswer();
            try
            {
                question = showNextQuestionOrScore();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                question.isError = true;
            }
            return View(question);
        }

        [HttpPost]
        //this is called when user submits a POST request of the answer they selected
        public string ProcessAnswer([FromBody]AnswerResult answer)
        {
            string returnText = "";
            try
            {
                using (_context)
                {
                    Answer dbAnswer = new Models.Answer(); //New instance of Answer class, representing one row in the DB's Answer table

                    //make sure the question id given is actually an int value potentially in the DB
                    if (Int32.Parse(answer.optionId) < 1)
                    {
                        throw new System.ArgumentException("Parameter must be greater than 0");
                    }

                    //update answer table, marking which question was answered
                    dbAnswer = _context.Answers.FirstOrDefault(a => a.Id == Int32.Parse(answer.optionId));
                    dbAnswer.WasSelected = true;
                    _context.Answers.Update(dbAnswer);

                    if (Int32.Parse(answer.questionId) < 1)
                    {
                        throw new System.ArgumentException("Parameter must be greater than 0");
                    }

                    //update question table, marking this question as answered
                    Question dbQuestion = new Models.Question();
                    dbQuestion = _context.Questions.FirstOrDefault(q => q.Id == Int32.Parse(answer.questionId));
                    dbQuestion.IsComplete = true;
                    _context.Questions.Update(dbQuestion);
                    _context.SaveChanges(); //Applies changes to DB
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                Response.StatusCode = 400;
                returnText = e.ToString();
            }
            return returnText;
        }

        //intializes the next question object, whether there is a question left or just the score
        public QuestionAnswer showNextQuestionOrScore()
        {
            QuestionAnswer currentQuestion = new QuestionAnswer();
            using (_context)
            {
                //get first question that has not been answered
                Question Question = _context.Questions.FirstOrDefault(q => q.IsComplete == false);
                //if the question exists, then fetch the corresponding answers
                if (Question != null)
                {
                    currentQuestion.questionText = Question.QuestionText;
                    currentQuestion.id = Question.Id;

                    //get answers associated with question
                    currentQuestion.answers = setAnswerOptionsForQuestion(currentQuestion.id);
                    currentQuestion.shuffleAnswers();
                }
                //if there is no answered question, the quiz is finished, so calculate the user score
                else
                {
                    //get all user selected answers options

                    var dbAnswers = (from c in _context.Answers select c).Where(p => p.WasSelected == true).ToList();
                    int questionCount = _context.Questions.Count(); //get number of questions answered

                    //calculate the percentage
                    currentQuestion.calculatePercentage(dbAnswers, questionCount);
                }
            }
            return currentQuestion;
        }

        //gets the answer options for a given question id
        private List<AnswerDisplay> setAnswerOptionsForQuestion(int questionId)
        {
            //initialize list of answer options
            var answers = new List<Models.Answer>();
            //get the answer options for this question from the DB
            answers = (from c in _context.Answers select c).Where(p => p.QuestionId == questionId).ToList();
            var answersViews = new List<AnswerDisplay>();

            //throw an exception if no answers were found
            if (answers.Count == 0)
            {
                throw new Exception("No answer options could be found for question id " + questionId);
            }

            foreach (Models.Answer answer in answers)
            {
                //define the properties of each viewmodel
                AnswerDisplay currentAnswer = new AnswerDisplay();
                currentAnswer.answerText = answer.AnswerText;
                currentAnswer.id = answer.Id;
                answersViews.Add(currentAnswer);
            }

            return answersViews;
        }
        public bool RestartQuiz()
        {
            using (_context)
            {
                var dbAnswer = new Models.Answer(); //New instance of Answer class, representing one row in the DB's Answer table

                //update answer table, marking which question was answered

                var answers = _context.Answers.ToList();
                answers.ForEach(a => a.WasSelected = null);


                //update question table, marking this question as answered
                var questions = _context.Questions.ToList();
                questions.ForEach(q => q.IsComplete = false);

                _context.SaveChanges(); //Applies changes to DB
            }
            
            return true;
        }

        //ASP.NET's default error catcher for unhandled exceptions
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
