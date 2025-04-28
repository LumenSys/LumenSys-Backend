using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LumenSys.Objects.Ultilities
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

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string ExtractNumbers(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return new string(text.Where(char.IsDigit).ToArray());
        }
        public static bool CompareString(string str1, string str2)
        {
            return string.Equals(str1.RemoveDiacritics(), str2.RemoveDiacritics(), StringComparison.OrdinalIgnoreCase);
        }
        public static string FormatForCurrency(double amount)
        {
            // Converte decimal para string com exatamente duas casas decimais
            string formattedAmount = amount.ToString("F2", CultureInfo.InvariantCulture);

            // Divide o número em parte inteira e parte decimal
            string[] parts = formattedAmount.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1].PadRight(2, '0') : "00";

            // Adiciona separadores de milhar à parte inteira
            integerPart = Regex.Replace(integerPart, @"(\d)(?=(\d{3})+(?!\d))", "$1.");

            // Retorna a string formatada com espaço
            return $"R$ {integerPart},{decimalPart}";
        }

        public static bool CheckValidPhone(string phone)
        {
            int phoneLength = OperatorUltilitie.ExtractNumbers(phone).Length;
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