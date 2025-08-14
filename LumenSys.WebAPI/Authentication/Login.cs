using LumenSys.WebAPI.Services.Utils;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace LumenSys.WebAPI.Authentication
{
    public class Login
    {
        [Required(ErrorMessage = "O e-mail é requerido!")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é requerida!")]
        [MinLength(9, ErrorMessage = "A senha deve conter pelo menos 9 caracteres.")]

        public string Password
        {
            get => password;
            set
            {
                password = value.GenerateHash();
            }
        }

        private string password;
    }
}
