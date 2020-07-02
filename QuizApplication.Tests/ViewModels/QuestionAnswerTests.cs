using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApplication.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using QuizApplication.Web.Models;

namespace QuizApplication.Web.ViewModels.Tests
{
    [TestClass]
    public class QuestionAnswerTests
    {
        [TestMethod]
        public void CalculatePercentageTest_OneTrueAnswer()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();
            Answer dbAnswer = new Answer()
            {
                IsCorrect = true,
                WasSelected = true
            };
            answers.Add(dbAnswer);
            questionAnswer.CalculatePercentage(answers, 1);
            Assert.AreEqual("100", questionAnswer.score);
        }

        [TestMethod]
        public void CalculatePercentageTest_OneFalseAnswer()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();
            Answer dbAnswer = new Answer()
            {
                IsCorrect = false,
                WasSelected = true
            };
            answers.Add(dbAnswer);
            questionAnswer.CalculatePercentage(answers, 1);
            Assert.AreEqual("0", questionAnswer.score);
        }

        [TestMethod]
        public void CalculatePercentageTest_SeveralWrongAndRight()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();
            int answerCount = 30;
            for(int i = 0; i < answerCount; i++)
            {
                Answer dbAnswer = new Answer()
                {
                    IsCorrect = false,
                    WasSelected = true
                };
                if (i == 29)
                {
                    dbAnswer.IsCorrect = true;
                }
                answers.Add(dbAnswer);
            }
            questionAnswer.CalculatePercentage(answers, answerCount);
            Assert.AreEqual("3", questionAnswer.score);
        }

        [TestMethod]
        public void CalculatePercentageTest_NoAnswer()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();

            questionAnswer.CalculatePercentage(answers, 1);
            Assert.AreEqual("0", questionAnswer.score);
        }

        [TestMethod]
        public void CalculatePercentageTest_NoQuestion()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();

            questionAnswer.CalculatePercentage(answers, 0);
            Assert.AreEqual("0", questionAnswer.score);
        }
    }
}