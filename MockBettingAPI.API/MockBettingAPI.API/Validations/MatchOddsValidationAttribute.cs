using MockBettingAPI.API.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MockBettingAPI.API.Validations
{
    public class MatchOddsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                MatchOddsToCreateDto matchOdds = (MatchOddsToCreateDto)validationContext.ObjectInstance;

                if (String.IsNullOrEmpty(matchOdds.Specifier) || 
                    (matchOdds.Specifier.ToLower() != "x" && matchOdds.Specifier != "1" && matchOdds.Specifier != "2" && matchOdds.Specifier.ToLower() != "o" && matchOdds.Specifier.ToLower() != "u"))
                    return new ValidationResult("Wrong Specifier. Please choose between '1', 'X', '2', 'U', 'O'");

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