using FluentValidation.Results;

namespace KH.Pepper.Services
{
    public class ValidationException1 : Exception
    {
        public ValidationException1(IEnumerable<ValidationFailure> failures)
            : base(string.Join(", ", failures))
        {
        }
    }
}
