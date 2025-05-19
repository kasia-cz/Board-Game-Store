using FluentValidation;

namespace BoardGameStore.Application.Validation.FluentValidation
{
    public class FluentValidationService<T> : IValidationService<T> where T : class
    {
        private readonly IValidator<T> _validator;

        public FluentValidationService(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void ValidateAndThrow(T model)
        {
            _validator.ValidateAndThrow(model);
        }
    }
}
