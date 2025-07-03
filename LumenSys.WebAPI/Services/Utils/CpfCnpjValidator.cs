using System.Linq;

namespace LumenSys.WebAPI.Services.Utils
{
    public static class CpfCnpjValidator
    {
        public static bool IsValid(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.ExtractNumbers();

            if (input.Length == 11)
                return IsValidCpf(input);

            if (input.Length == 14)
                return IsValidCnpj(input);

            return false;
        }

        public static bool IsValidCpf(string cpf)
        {
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            var digits = cpf.Select(c => c - '0').ToArray();

            int sum1 = 0;
            for (int i = 0; i < 9; i++)
                sum1 += digits[i] * (10 - i);

            int check1 = sum1 % 11;
            check1 = check1 < 2 ? 0 : 11 - check1;

            if (digits[9] != check1)
                return false;

            int sum2 = 0;
            for (int i = 0; i < 10; i++)
                sum2 += digits[i] * (11 - i);

            int check2 = sum2 % 11;
            check2 = check2 < 2 ? 0 : 11 - check2;

            return digits[10] == check2;
        }

        public static bool IsValidCnpj(string cnpj)
        {
            if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1)
                return false;

            var digits = cnpj.Select(c => c - '0').ToArray();
            int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int sum1 = 0;
            for (int i = 0; i < 12; i++)
                sum1 += digits[i] * mult1[i];

            int check1 = sum1 % 11;
            check1 = check1 < 2 ? 0 : 11 - check1;

            if (digits[12] != check1)
                return false;

            int sum2 = 0;
            for (int i = 0; i < 13; i++)
                sum2 += digits[i] * mult2[i];

            int check2 = sum2 % 11;
            check2 = check2 < 2 ? 0 : 11 - check2;

            return digits[13] == check2;
        }
    }
}
