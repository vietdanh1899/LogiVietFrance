using BT04_2022.Data.Models;
using BT04_2022.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BT04_2022.Data.Services
{
    public class QuizServices
    {
        AppDbContext context;
        public QuizServices(AppDbContext _context)
        {
            context = _context;
        }

        public async Task AddQuiz(QuizVM quiz)
        {
            var newQuiz = new Quiz()
            {
                Question = quiz.Question,
                Answers = new List<Answer>()
            };
            context.Quizzes.Add(newQuiz);
            foreach (var answer in quiz.Answers)
            {
                Answer answer1 = new Answer()
                {
                    Value = answer.Value,
                    IsTrue = answer.IsTrue,
                };
                newQuiz.Answers.Add(answer1);
            }
            await context.SaveChangesAsync();
            return;
        }

        public async Task<List<Quiz>> GetAllQuizzes()
        {
            return await context.Quizzes.Include(quiz => quiz.Answers).ToListAsync();
        }

        public async Task<Quiz> GetQuizById(int id)
        {
            return await context.Quizzes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task ReplaceQuiz(int Id, QuizVM quiz)
        {
            var _quiz = await context.Quizzes.Include(quiz => quiz.Answers).FirstOrDefaultAsync(i => i.Id == Id);
            if (_quiz != null)
            {
                _quiz.Question = quiz.Question;
                _quiz.Answers.Clear();
                foreach (var answer in quiz.Answers)
                {
                    Answer a = new Answer()
                    {
                        Value = answer.Value,
                        IsTrue = answer.IsTrue
                    };
                    _quiz.Answers.Add(a);
                }
                await context.SaveChangesAsync();
            }
            return;
        }
    }
}
