using BT04_2022.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BT04_2022.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
