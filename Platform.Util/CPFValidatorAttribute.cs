using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Platform.Util
{
    public class CPFValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = false;

            if (value == null)
                return true;

            if (value == null)
                return true;

            value = Regex.Replace(value.ToString(), "[^0-9]+", "");
            if (value.ToString().Count() != 11)
                return false;

            if (value.ToString().Count() == 11)
                result = Validator.IsCPF(value.ToString());
            
            return result;
        }

        public bool IsValid(string value)
        {
            bool result = false;

            if (value == null)
                return true;

            if (value == null)
                return true;

            value = Regex.Replace(value.ToString(), "[^0-9]+", "");
            if (value.ToString().Count() != 11)
                return false;

            if (value.ToString().Count() == 11)
                result = Validator.IsCPF(value.ToString());
            
            return result;
        }
    }
}
