using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SF11_2019_POP2020.Validations
{
    public class EmailValidacija : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Contains("@") && value.ToString().EndsWith(".com"))
                return new ValidationResult(true, "");
            return new ValidationResult(false, "Email treba da bude u ispravnom formatu!");
            

            
        }
    }
}
