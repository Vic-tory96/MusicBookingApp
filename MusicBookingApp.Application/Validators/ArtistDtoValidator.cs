using FluentValidation;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Validators
{
    public class ArtistDtoValidator : AbstractValidator<ArtistDto>
    {
        public ArtistDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Genre is required.")
                .Length(2, 50).WithMessage("Genre must be between 2 and 50 characters.");

            RuleFor(x => x.Bio)
                .MaximumLength(500).WithMessage("Bio can be at most 500 characters.");
        }
    }
}
