using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Укажите ваше имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Укажите вашу фамилию")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
