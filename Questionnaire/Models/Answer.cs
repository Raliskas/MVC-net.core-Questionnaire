using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Models
{
    public class Answer //Ответы пользователя
    {
        public int AnswerID { get; set; }
        public int UserID { get; set; }
        public int TestID { get; set; }
        public int QuestionNumber { get; set; }
        public bool UserChoices { get; set; }
        public int ChoiceID { get; set; }
        public Choice Choice { get; set; }


    }
}
