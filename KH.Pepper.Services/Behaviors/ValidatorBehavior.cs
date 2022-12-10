using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KH.Pepper.Core.AppServices
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(ILogger<ValidatorBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }
         
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Begin validation of {request}");

            var context = new ValidationContext<TRequest>(request);

            var validations = _validators.Select(x => x.ValidateAsync(context, cancellationToken)).ToList();

            await Task.WhenAll(validations);

            var failures = validations.Select(x => x.Result).SelectMany(result => result.Errors)
                            .Where(f => f != null)
                            .ToList();

            if (failures.Count != 0)
            {
                _logger.LogInformation($"validation of {request} failed");
                throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
