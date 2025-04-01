using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.EventId)
            .NotEmpty().WithMessage("EventId is required");

            RuleFor(b => b.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }
}
