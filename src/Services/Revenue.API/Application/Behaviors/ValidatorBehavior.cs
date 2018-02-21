using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Revenue.Domain.Exceptions;

namespace Microservices.Services.Revenue.API.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidatorBehavior(IValidator<TRequest> validator) => _validator = validator;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = await _validator.ValidateAsync(request);

            if (result.Errors.Any())
            {
                throw new RevenueDomainException(
                    $"Command validation errors for type {typeof(TRequest).Name}",
                        new ValidationException("Validation exception", result.Errors));
            }

            var response = await next();
            return response;
        }
    }
}