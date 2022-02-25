using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT04_2022.Data.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        public string Question { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
