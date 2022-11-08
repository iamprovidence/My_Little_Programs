using CSharpFunctionalExtensions;
using Mediator;
using IResult = CSharpFunctionalExtensions.IResult;

namespace MediatorTest.Application.Common
{
    // no IRequestExceptionHandler, sadly =(
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : notnull, IMessage
        where TResponse : IResult<object, ErrorObject>
    {
        public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
        {
            try
            {
                return await next(message, cancellationToken);
            }
            catch (ValidationException ex)
            {
                var error = ErrorObject.Create(ApplicationErrorCode.Validation, string.Join(", ", ex.Errors));
                var result = CreateErrorResult(error);

                return (TResponse)result;
            }
        }

        private static IResult CreateErrorResult(ErrorObject error)
        {
            var resultResponseType = typeof(TResponse).GenericTypeArguments.First();
            var methodInfo = typeof(Result).GetMethods()
                .Where(m => m.Name == nameof(Result.Failure))
                .Where(m => m.GetGenericArguments().Length == 2)
                .Single()
                .MakeGenericMethod(new Type[] { resultResponseType, typeof(ErrorObject) });

            var result = methodInfo.Invoke(null, new[] { error })!;

            return (IResult)result;
        }
    }
}
