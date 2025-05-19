using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BoardGameStore.Application.Validation.DataAnnotations
{
    public class DataAnnotationsValidationService<T> : IValidationService<T>
    {
        public void ValidateAndThrow(T model)
        {
            var results = new List<ValidationResult>();
            //var context = new ValidationContext(model, null, null);
            //bool isValid = Validator.TryValidateObject(model, context, results, true);
            bool isValid = TryValidateRecursive(model, results);

            if (!isValid)
            {
                throw new Exception(string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage)));
            }
        }

        private bool TryValidateRecursive(object obj, List<ValidationResult> results)
        {
            if (obj == null)
                return true;

            var context = new ValidationContext(obj, null, null);
            bool isValid = Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

            var properties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value == null)
                    continue;

                var type = property.PropertyType;

                if (type == typeof(string) || type.IsValueType)
                    continue;

                if (value is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        isValid &= TryValidateRecursive(item, results);
                    }
                }
                else
                {
                    isValid &= TryValidateRecursive(value, results);
                }
            }

            return isValid;
        }
    }
}
