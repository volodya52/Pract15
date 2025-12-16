using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.Validators
{
    public class CheckDate:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value is DateOnly date)
            {
                if(date > DateOnly.FromDateTime(DateTime.Today))
                {
                    return new ValidationResult(false, "Дата не может быть будущей");
                }
                return ValidationResult.ValidResult;
            }
            return ValidationResult.ValidResult;
        }
    }
}
