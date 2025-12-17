using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.Validators
{
    public class CheckRating:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                if(!decimal.TryParse(value.ToString(), out decimal rating))
                {
                    return new ValidationResult(false, "Рейтинг должен быть числом");
                }

                if (rating < 0)
                {
                    return new ValidationResult(false, "Рейтинг не должен быть меньше нуля");
                }
                return ValidationResult.ValidResult;
            }
            return ValidationResult.ValidResult;


        }
    }
}
