using System.ComponentModel.DataAnnotations;

namespace ehrApi.Validation;

public class DateOfBirthValidator : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var dateCutOff = today.AddYears(-200);

            return date >= dateCutOff && date <= today;
        }

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return "Date of birth must be DateOnly format 'yyyy-mm-dd' cannot be 200 years in the past or in the future.";
    }
}