using ChuckIt.Core.Entities.Listings.Dtos;

using FluentValidation;

namespace ChuckItApiV2.Validators.Listings
{
    public class CreateListingValidator : AbstractValidator<CreateListingDto>
    {
        public CreateListingValidator()
        {
            RuleFor(x => x.ImageFileName)
                .Must(i => i.Count > 0)
                .WithMessage($"{nameof(CreateListingDto.ImageFileName)}: An Image is required");

            RuleFor(x => x.Title)
                .Must(t => !string.IsNullOrEmpty(t))
                .WithMessage($"{nameof(CreateListingDto.Title)}: Title is required");

            RuleFor(x => x.Price)
                .Must(p => p > 0)
                .WithMessage($"{nameof(CreateListingDto.Price)}: Price must be greater than 0");

            RuleFor(x => x.Description)
                .Must(d => !string.IsNullOrEmpty(d))
                .WithMessage($"{nameof(CreateListingDto.Description)}: A description is required");

            RuleFor(x => x.CategoryId)
                .Must(c => c > 0)
                .WithMessage($"{nameof(CreateListingDto.CategoryId)}: Category Id must be 1 or above");
        }
    }
}
