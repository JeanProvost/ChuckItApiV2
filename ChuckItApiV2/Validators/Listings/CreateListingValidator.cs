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
        }
    }
}
