using System;
using System.ComponentModel.DataAnnotations;

namespace Book_Management_API.Attributes
{
    public class NotFutureYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int year && year > DateTime.Now.Year)
            {
                return new ValidationResult("Publication year cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
