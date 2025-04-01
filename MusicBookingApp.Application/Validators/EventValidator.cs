using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Validators
{
    public class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Event title is required")
                .MaximumLength(200).WithMessage("Event title must be less than 200 characters");

            RuleFor(e => e.Date)
                .GreaterThan(DateTime.UtcNow).WithMessage("Event date must be in the future");

            RuleFor(e => e.Location)
                .NotEmpty().WithMessage("Location is required");
        }
    }
}
