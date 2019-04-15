using System;
using FluentValidation;
using Microservices.Services.Revenue.API.Application.Commands;

namespace Microservices.Services.Revenue.API.Application.Validations
{
    public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            RuleFor(command => command.BusId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);

            RuleFor(command => command.DriverId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);

            RuleFor(command => command.ConductorId)
                .NotNull()
                .NotEmpty()
                .NotEqual(Guid.Empty);

            RuleFor(command => command.TripDate)
                .NotEmpty()
                .NotEqual(default(DateTime));

            RuleForEach(command => command.TripLegs)
                .SetValidator(new TripLegValidator());
        }
    }

    public class TripLegValidator : AbstractValidator<TripLegDto>
    {
        public TripLegValidator()
        {
            RuleFor(tripLeg => tripLeg.Route)
                .NotEmpty()
                .NotNull();

            RuleFor(tripLeg => tripLeg.Revenue)
                .GreaterThanOrEqualTo(0.0M);
        }
    }
}