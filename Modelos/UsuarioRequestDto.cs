using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos
{
    public class UsuarioRequestDto
    {
        [Required(ErrorMessage = "Name es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email es requerido")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Password es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword es requerido")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password y ConfirmPassword no son iguales")]
        public string ConfirmPassword { get; set; }
    }
}
