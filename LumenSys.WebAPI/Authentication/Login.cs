using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace api.Authentication
{
    public class Login
    {
        [Required(ErrorMessage = "O e-mail é requerido!")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é requerida!")]
        [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres.")]
        public string? PlainPassword { get; set; }

        public string HashedPassword => GenerateHash(PlainPassword ?? string.Empty);

        private static string GenerateHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return string.Empty;

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
