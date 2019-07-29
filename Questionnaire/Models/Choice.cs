using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Models
{
    public class Choice
    {
        public int ChoiceID { get; set; }
        public int TestID { get; set; }
        public int QuestionNumber { get; set; }
        [Required(ErrorMessage = "Введите текст ответа")]
        public string ChoiceText { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
        [ForeignKey("QuestionID")]
        public int QuestionID { get; set; }
        public List<Answer> Answers { get; set; }
    }
   
}
