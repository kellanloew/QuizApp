using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApplication.Models;
using QuizApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApplication.ViewModels.Tests
{
    [TestClass()]
    public class QuestionAnswerTests
    {
        [TestMethod()]
        public void calculatePercentageTest_OneTrueAnswer()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();
            Answer dbAnswer = new Answer();
            dbAnswer.IsCorrect = true;
            dbAnswer.WasSelected = true;
            answers.Add(dbAnswer);
            questionAnswer.calculatePercentage(answers, 1);
            Assert.AreEqual("100", questionAnswer.score);
        }

        [TestMethod()]
        public void calculatePercentageTest_OneFalseAnswer()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();
            Answer dbAnswer = new Answer();
            dbAnswer.IsCorrect = false;
            dbAnswer.WasSelected = true;
            answers.Add(dbAnswer);
            questionAnswer.calculatePercentage(answers, 1);
            Assert.AreEqual("0", questionAnswer.score);
        }

        [TestMethod()]
        public void calculatePercentageTest_SeveralWrongAndRight()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();
            int answerCount = 30;
            for(int i = 0; i < answerCount; i++)
            {
                Answer dbAnswer = new Answer();
                dbAnswer.IsCorrect = false;
                dbAnswer.WasSelected = true;
                if(i == 29)
                {
                    dbAnswer.IsCorrect = true;
                }
                answers.Add(dbAnswer);
            }
            questionAnswer.calculatePercentage(answers, answerCount);
            Assert.AreEqual("3", questionAnswer.score);
        }
        [TestMethod()]
        public void calculatePercentageTest_NoAnswer()
        {
            QuestionAnswer questionAnswer = new QuestionAnswer();
            var answers = new List<Answer>();

            questionAnswer.calculatePercentage(answers, 1);
            Assert.AreEqual("0", questionAnswer.score);
        }

        //[TestMethod()]
        //public void calculatePercentageTest_NoQuestion()
        //{
        //    QuestionAnswer questionAnswer = new QuestionAnswer();
        //    var answers = new List<Answer>();

        //    questionAnswer.calculatePercentage(answers, 1);
        //    Assert.AreEqual("0", questionAnswer.score);
        //}
    }
}