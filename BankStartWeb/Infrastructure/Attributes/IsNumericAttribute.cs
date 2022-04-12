using System.ComponentModel.DataAnnotations;

namespace Bank_AB.Infrastructure.Attributes
{
    public class IsNumericAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal val;
                var isNumeric = decimal.TryParse(value.ToString(), out val);

                if (!isNumeric)
                {
                    return new ValidationResult("Must be numeric");
                }
            }

            return ValidationResult.Success;
        }
    }
}
