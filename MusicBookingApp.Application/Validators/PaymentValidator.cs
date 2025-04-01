using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MusicBookingApp.Domain.Entities;

namespace MusicBookingApp.Application.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than zero.");

            RuleFor(p => p.PaymentStatus)
                .IsInEnum().WithMessage("Invalid payment status.");

            RuleFor(p => p.PaymentMethod)
                .IsInEnum().WithMessage("Invalid payment method.");

            RuleFor(p => p.TransactionId)
                .NotEmpty().WithMessage("Transaction ID is required.")
                .MaximumLength(50).WithMessage("Transaction ID must not exceed 50 characters.");

            RuleFor(p => p.PaymentDate)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Payment date cannot be in the future.");
        }
    }
}

