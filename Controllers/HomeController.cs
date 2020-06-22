using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuizApplication.Models;
using QuizApplication.ViewModels;

namespace QuizApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly QuizApplicationContext _context;

        public HomeController(QuizApplicationContext context)
        {
            //_logger = logger;
            _context = context;
        }

        public ActionResult Index()
        {
            
            return View(getNextQuestion());
        }

        [HttpPost]
        public string ProcessAnswer([FromBody]AnswerResult answer)
        {
            return "You selected answer #" + answer.optionId;
        }

        public QuestionAnswer getNextQuestion()
        {
            QuestionAnswer currentQuestion = new QuestionAnswer();
            currentQuestion.questionText = "Here is a question";
            currentQuestion.answers = new List<Answer>();
            currentQuestion.id = 1;
            for (int i = 1; i < 4; i++)
            {
                Answer currentAnswer = new Answer();
                currentAnswer.answerText = "This is answer option " + i;
                currentAnswer.id = i;
                currentQuestion.answers.Add(currentAnswer);
            }
            return currentQuestion;
        }

        [HttpGet]
        public IActionResult Details(int? id = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionItem = _context.QuestionItems
                .FirstOrDefault(m => m.Id == id);
            if (questionItem == null)
            {
                return NotFound();
            }

            QuestionAnswer myViewModel = new QuestionAnswer();
            myViewModel.questionText = questionItem.QuestionText;

            return View(myViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
