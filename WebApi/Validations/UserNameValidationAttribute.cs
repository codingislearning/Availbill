using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UserNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string email = (string)validationContext.ObjectType.GetProperty("Email").GetValue(validationContext.ObjectInstance, null);

            string mobileNumber = (string)validationContext.ObjectType.GetProperty("MobileNumber").GetValue(validationContext.ObjectInstance, null);

            //check at least one has a value
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(mobileNumber))
                return new ValidationResult("At least one is required!!");

            return ValidationResult.Success;
        }
    }
}