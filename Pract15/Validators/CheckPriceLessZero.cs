using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.Validators
{
    public class CheckPriceLessZero:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value!=null && !string.IsNullOrEmpty(value.ToString()))
            {
                if(!decimal.TryParse(value.ToString(), out decimal price))
                {
                    return new ValidationResult(false, "Цена должна быть числом");
                }

                if (price <= 0)
                {
                    return new ValidationResult(false, "Цена не должна быть меньше или равной нулю");
                }
                return ValidationResult.ValidResult;
            }
            return ValidationResult.ValidResult;
        }
    }
}
