using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SF11_2019_POP2020.Validations
{
    class JmbgValidacija : ValidationRule
    {
       

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Trim().Length == 13)
                return new ValidationResult(true, "");
            return new ValidationResult(false, "Jmbg treba da bude u ispravnom JMBG formatu(13 cifara)");
        }
    }
}
