using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Models
{
    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int TestID { get; set; }
        [Required(ErrorMessage = "Введите название теста")]
        public string TestName { get; set; }
        [Required(ErrorMessage = "Заполните описание для теста")]
        public string TestDescription { get; set; }

        public int AuthorID { get; set; }
        
    }
}
