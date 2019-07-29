using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Models
{
    public class User
    {
        public int ID { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [DisplayName("Имя")]
        [Required(ErrorMessage = "Введите Имя пользователя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Введите Фамилию пользователя")]
        public string LastName { get; set; }

        [DisplayName("Дата рождения")]
        [Required(ErrorMessage = "Выберете дату рожд. пользователя")]
        [DataType(DataType.Date,ErrorMessage ="Дата указана не верно")]
        public DateTime? BirthDate { get; set; }
        public int? RoleId { get; set; }

        [DisplayName("Роль")]
       
        public Role Role { get; set; }
        [DisplayName("E-mail")]
        [Required(ErrorMessage = "Введите Е-mail пользователя")]
        public string Email { get; set; }
        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Введите пароль пользователя")]
        public string Password { get; set; }


    }
}
