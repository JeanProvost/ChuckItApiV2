using ChuckIt.Core.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace ChuckItApiV2.Validators
{
    public class ValidatorExtensions
    {
        public static void ThrowModelValidation(FluentValidation.Results.ValidationResult validationResult)
        {
            List<string> validationErrors = new List<string>();

            foreach (var error in validationResult.Errors)
            {
                validationErrors.Add(error.ErrorMessage);
            }

            throw new ModelValidationException(validationErrors);
        }

        public static void ValidateAndThrowIfInvalid<TValidator, TModel>(TValidator validator, TModel model)
            where TValidator : IValidator<TModel>
        {
            var validationResult = validator.Validate(model); 

            if (!validationResult.IsValid)
            {
                ThrowModelValidation(validationResult);
            }
        }
    }
}
