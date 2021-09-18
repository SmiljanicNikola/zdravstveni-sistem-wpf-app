using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SF11_2019_POP2020.Validations
{
    class AdresaValidacija : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!value.ToString().Equals("") && !value.ToString().Contains("1") && !value.ToString().Contains("2") && !value.ToString().Contains("3") && !value.ToString().Contains("4")
                && !value.ToString().Contains("5") && !value.ToString().Contains("6") && !value.ToString().Contains("7") && !value.ToString().Contains("8") && !value.ToString().Contains("9")
                && !value.ToString().Contains("0"))
                return new ValidationResult(true, "");
            return new ValidationResult(false, "Ovo polje je obavezno za uneti i mora biti ISKLJUCIVO TEKST!");
        }
    }
}
