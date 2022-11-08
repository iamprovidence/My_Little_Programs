using CSharpFunctionalExtensions;
using FluentValidation.Results;

namespace MediatorTest.Application.Common
{
    public class ValidationException : Exception
    {
        public IReadOnlyCollection<string> Errors { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures.Select(x => x.ErrorMessage).ToList();
        }
    }
}
