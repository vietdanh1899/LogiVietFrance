using BT04_2022.Controllers;
using BT04_2022.Data;
using BT04_2022.Data.Models;
using BT04_2022.Data.Services;
using BT04_2022.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestBT04_2022
{

    public class TestQuizController : IDisposable
    {
        private SqliteConnection _connection;
        private AppDbContext _context;
        private QuizServices _quizServices;
        private QuizController _quizController;

        public TestQuizController()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _context = CreateContext();
            _quizServices = new QuizServices(_context);
            _quizController = new QuizController(_quizServices);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public AppDbContext CreateContext()
        {
            var _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;
            var context = new AppDbContext(_contextOptions);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async void Test_POST_AddQuiz()
        {
            IActionResult actionResult = await _quizController.AddQuiz(new QuizVM()
            {
                Question = "Who am I?",
                Answers = new List<AnswerVM>()
                {
                    new AnswerVM(){Value = "Danh", IsTrue = true},
                    new AnswerVM(){Value = "Đức", IsTrue = false},
                    new AnswerVM(){Value = "Giang", IsTrue = false}
                }
            });
            Assert.IsType<OkResult>(actionResult);
            var quiz = await _context.Quizzes.Include(quiz => quiz.Answers)
                .FirstAsync(quiz => quiz.Question == "Who am I?");
            Assert.NotNull(quiz);
            Assert.NotNull(quiz.Answers);
            Assert.Equal("Danh", quiz.Answers.First().Value);
            Assert.True(quiz.Answers.First().IsTrue);
        }

        [Fact]
        public async void Test_GET_GetQuizById_ReturnOk()
        {
            _context.Quizzes.Add(
                new Quiz() { Question = "abc", Answers = new List<Answer>() }
            );
            _context.SaveChanges();
            IActionResult actionResult = await _quizController.GetQuizById(1);
            Assert.IsType<OkObjectResult>(actionResult);
            var quiz = (actionResult as OkObjectResult).Value as Quiz;
            Assert.Equal(1, quiz.Id);
            Assert.Equal("abc", quiz.Question);
        }

        [Fact]
        public async void Test_GET_GetQuizById_ReturnNotFound()
        {
            IActionResult actionResult = await _quizController.GetQuizById(1);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void Test_GET_GetAllQuizzes()
        {
            _context.Quizzes.AddRange(
                new Quiz()
                {
                    Question = "Who am I?",
                    Answers = new List<Answer>()
                {
                    new Answer(){Value = "Danh", IsTrue = true},
                    new Answer(){Value = "Đức", IsTrue = false},
                    new Answer(){Value = "Giang", IsTrue = false}
                }
                },
                new Quiz()
                {
                    Question = "Who am I?",
                    Answers = new List<Answer>()
                    {
                        new Answer(){Value = "Danh", IsTrue = true},
                        new Answer(){Value = "Đức", IsTrue = false},
                        new Answer(){Value = "Giang", IsTrue = false}
                    }
                }
            );
            _context.SaveChanges();
            IActionResult actionResult = await _quizController.GetAllQuizzes();
            Assert.IsType<OkObjectResult>(actionResult);
            var quizzes = (actionResult as OkObjectResult).Value as List<Quiz>;
            Assert.Equal(2, quizzes.Count);
        }

        [Fact]
        public async void Test_ReplaceQuiz()
        {
            _context.Quizzes.Add(
                new Quiz() { Question = "abc", Answers = new List<Answer>() }
            );
            _context.SaveChanges();
            IActionResult actionResult = await _quizController.ReplaceQuiz(1,
                new QuizVM()
                {
                    Question = "Who am I?",
                    Answers = new List<AnswerVM>()
                {
                    new AnswerVM(){Value = "Danh", IsTrue = true},
                    new AnswerVM(){Value = "Đức", IsTrue = false},
                    new AnswerVM(){Value = "Giang", IsTrue = false}
                }
                });
            Assert.IsType<OkResult>(actionResult);
            var quiz = _context.Quizzes.First(quiz => quiz.Id == 1);
            Assert.Equal("Who am I?", quiz.Question);
        }
    }
}