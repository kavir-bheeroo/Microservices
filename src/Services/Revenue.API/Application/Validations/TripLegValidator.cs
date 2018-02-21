using FluentValidation;
using Microservices.Services.Revenue.API.Application.Commands;

namespace Microservices.Services.Revenue.API.Application.Validations
{
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