using BT04_2022.Data.Services;
using BT04_2022.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BT04_2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        QuizServices quizServices;
        public QuizController(QuizServices quizServices)
        {
            this.quizServices = quizServices;
        }

        /// <summary>
        /// Add a Quiz
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [HttpPost]
        public async  Task<IActionResult> AddQuiz([FromBody]QuizVM quiz)
        {
            await quizServices.AddQuiz(quiz);
            return Ok();
        }

        /// <summary>
        /// Get all Quizzes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var allQuizzes = await quizServices.GetAllQuizzes();
            return Ok(allQuizzes);
        }

        /// <summary>
        /// Get a Quiz by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizById(int id)
        {
            var quiz = await quizServices.GetQuizById(id);
            if (quiz != null)
            {
                return Ok(quiz);
            }
            return NotFound();
        }

        /// <summary>
        /// Replace a Quiz
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> ReplaceQuiz(int id, [FromBody]QuizVM quiz)
        {
            await quizServices.ReplaceQuiz(id, quiz);
            return Ok();
        }
    }
}
