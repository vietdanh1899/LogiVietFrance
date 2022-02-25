using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BT04_2022.Data.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }

        public bool IsTrue { get; set; }

        [ForeignKey("QuizId")]
        [Required]
        [JsonIgnore]
        public Quiz Quiz { get; set; }
    }
}
