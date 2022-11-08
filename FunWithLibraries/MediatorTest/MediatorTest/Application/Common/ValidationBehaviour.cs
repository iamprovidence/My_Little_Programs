using FluentValidation;
using Mediator;

namespace MediatorTest.Application.Common
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : notnull, IMessage
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ValidationBehaviour(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }


        public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var validators = scope.ServiceProvider.GetRequiredService<IEnumerable<IValidator<TRequest>>>();

                if (validators.Any())
                {
                    var context = new ValidationContext<TRequest>(message);
                    var validationResults = await Task.WhenAll(
                        validators.Select(v =>
                            v.ValidateAsync(context, cancellationToken)));

                    var failures = validationResults
                        .Where(r => r.Errors.Any())
                        .SelectMany(r => r.Errors)
                        .ToList();

                    if (failures.Any())
                    {
                        throw new ValidationException(failures);
                    }
                }
                return await next(message, cancellationToken);
            }
        }
    }
}
