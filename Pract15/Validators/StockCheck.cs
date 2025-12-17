using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pract15.Validators
{
    public class StockCheck:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value!=null && !string.IsNullOrEmpty(value.ToString()))
            {
                if(!int.TryParse(value.ToString(), out int stock))
                {
                    return new ValidationResult(false, "Количество должно быть числом");
                }

                if (stock <= 0)
                {
                    return new ValidationResult(false, "Количество должно быть больше нуля");
                }
                return ValidationResult.ValidResult;
            }
            return ValidationResult.ValidResult;
        }
    }
}
