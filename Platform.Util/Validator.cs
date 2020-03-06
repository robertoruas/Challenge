using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Util
{
    public static class Validator
    {
        public static bool IsCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            if (cpf == "00000000000" ||
                cpf == "11111111111" ||
                cpf == "22222222222" ||
                cpf == "33333333333" ||
                cpf == "44444444444" ||
                cpf == "55555555555" ||
                cpf == "66666666666" ||
                cpf == "77777777777" ||
                cpf == "88888888888" ||
                cpf == "99999999999")
                return false;

            int[] multiplicator1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicator2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int mod;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11) return false;
            tempCpf = cpf.Substring(0, 9); sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicator1[i]; mod = sum % 11;
            if (mod < 2)
                mod = 0;
            else
                mod = 11 - mod;
            digit = mod.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicator2[i];
            mod = sum % 11;
            if (mod < 2)
                mod = 0;
            else
                mod = 11 - mod;
            digit = digit + mod.ToString();
            
            return cpf.EndsWith(digit);
        }
    }
}
