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
            if (value == null)
            {
                return new ValidationResult(false, "Дата не должна быть пустой");
            }

            if(value is DateTime date)
            {
                var dateTime=DateOnly.FromDateTime(date);
                var today = DateOnly.FromDateTime(DateTime.Now);
                if (dateTime > today)
                {
                    return new ValidationResult(false, "Дата создания не может быть в будущем");
                }
                return ValidationResult.ValidResult;
            }
            return ValidationResult.ValidResult;
        }
    }
}
