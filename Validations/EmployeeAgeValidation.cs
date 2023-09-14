using System.ComponentModel.DataAnnotations;

namespace DMAWS_T2204M_NguyenHoangLong.Validations
{
    public class EmployeeAgeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dob)
            {
                var currentDate = DateTime.Today;
                var age = currentDate.Year - dob.Year;

                // check age, minus 1 if invalid
                if (dob.Date > currentDate.AddYears(-age))
                {
                    age--;
                }

                const int minAgeRequired = 16;

                if (age < minAgeRequired)
                {
                    return new ValidationResult("Employee must be over 16 years old.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Employee DOB must be a date type.");
        }
    }
}
