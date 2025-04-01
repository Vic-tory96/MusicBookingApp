using FluentValidation;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Validators
{
    public class ArtistValidator : AbstractValidator<Artist>
    {
        public ArtistValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Artist name is required")
                .MaximumLength(100).WithMessage("Artist name must be less than 100 characters");

            RuleFor(a => a.Genre)
                .NotEmpty().WithMessage("Genre is required");

            RuleFor(a => a.Bio)
                .MaximumLength(500).WithMessage("Bio must be less than 500 characters");
        }
    }
}
