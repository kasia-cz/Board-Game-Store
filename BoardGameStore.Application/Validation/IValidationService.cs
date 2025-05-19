namespace BoardGameStore.Application.Validation
{
    public interface IValidationService<T>
    {
        void ValidateAndThrow(T model);
    }
}
