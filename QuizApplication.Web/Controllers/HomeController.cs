using System;
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
                question.ShowNextQuestionOrScore(_context);
            }
            catch (Exception e)
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

        [HttpPost]
        //called when user wants to delete all questions they've answered and start the quiz over
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
