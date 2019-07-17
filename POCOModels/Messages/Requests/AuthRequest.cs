using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POCOModels.Messages.Requests
{
    public class AuthRequest
    {
        [Required]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пароль не должен быть короче 6 символов", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
