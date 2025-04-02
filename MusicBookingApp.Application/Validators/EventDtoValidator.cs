using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MusicBookingApp.Application.Dto;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Validators
{
    public class EventDtoValidator : AbstractValidator<EventDto>
    {
        public EventDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Event title is required.");

            RuleFor(x => x.Date)
                .GreaterThan(DateTime.Now).WithMessage("Event date must be in the future.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Event location is required.");

            RuleFor(x => x.ArtistId)
                .NotEmpty().WithMessage("Artist ID is required.");
        }
    }
}
