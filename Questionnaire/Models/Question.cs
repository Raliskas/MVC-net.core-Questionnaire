using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Questionnaire.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        [Required(ErrorMessage = "Введите текст вопроса")]
        public string QuestionText { get; set; }
        public int QuestionNumber { get; set; }
        public int TestID { get; set; }           
        public List<Choice> Choices { get; set; }
        
    }
   
}
