using System.ComponentModel.DataAnnotations;

namespace Book_Management_API.Attributes
{
    public class NotSetIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int id && id != 0)
            {
                return new ValidationResult("Id should not be set manually, it should remain with default value.");
            }
            return ValidationResult.Success;
        }
    }
}
