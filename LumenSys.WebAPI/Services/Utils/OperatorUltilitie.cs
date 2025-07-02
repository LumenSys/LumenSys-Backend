using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LumenSys.WebAPI.Services.Utils
{
    public static class OperatorUltilitie
    {
        public static string GenerateHash(this string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        public static string FormatForCurrency(double amount)
        {
            string formattedAmount = amount.ToString("F2", CultureInfo.InvariantCulture);

            string[] parts = formattedAmount.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1].PadRight(2, '0') : "00";

            integerPart = Regex.Replace(integerPart, @"(\d)(?=(\d{3})+(?!\d))", "$1.");

            return $"R$ {integerPart},{decimalPart}";
        }

        public static bool CheckValidPhone(string phone)
        {
            int phoneLength = StringUtils.ExtractNumbers(phone).Length;
            return phoneLength > 9 && phoneLength < 12;
        }

        public static int CheckValidEmail(string email)
        {
            // Verifica se há um único "@" e que não está no início ou no final
            int atCount = email.Count(c => c == '@');
            bool hasTextBeforeAt = email.IndexOf('@') > 0;
            bool hasTextAfterAt = email.LastIndexOf('@') < email.Length - 1;

            // Verifica se após o "@" há um "." e se não termina com "."
            int atPosition = email.IndexOf('@');
            bool hasDotAfterAt = atPosition >= 0 && email.IndexOf('.', atPosition) > atPosition;
            bool endsWithDot = email.EndsWith('.');

            // Verificações
            if (atCount != 1) return -1; // E-mail inteiro inválido

            else if (!hasTextBeforeAt) return -1; // Parte antes do @ inválida

            else if (!hasTextAfterAt) return -2; // Domínio inválido

            else if (!hasDotAfterAt) return -2; // Domínio inválido

            else if (endsWithDot) return -1; // E-mail inteiro inválido

            return 1; // E-mail válido
        }

    }
}