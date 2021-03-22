using MockBettingAPI.API.Models;
using MockBettingAPI.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace MockBettingAPI.API.Validations
{
    public class MatchValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                MatchToCreateDto match = (MatchToCreateDto)validationContext.ObjectInstance;

                bool valid = DateTime.TryParse(match.MatchDate, out DateTime matchDate);
                if (!valid)
                    return new ValidationResult("Unreadable match date");

                valid = TimeSpan.TryParse(match.MatchTime, out TimeSpan matchTime);
                if (!valid)
                    return new ValidationResult("Unreadable match time");

                if (!Enum.IsDefined(typeof(Sport), match.Sport))
                    return new ValidationResult("Nonexistent sport");

                if (String.IsNullOrEmpty(match.TeamA))
                    return new ValidationResult("Please add a TeamA");
                if (String.IsNullOrEmpty(match.TeamB))
                    return new ValidationResult("Please add a TeamB");

                return ValidationResult.Success;
            }
            catch (Exception)
            {
                // log exception
                return new ValidationResult("Model Validation Error");
            }
        }
    }
}
