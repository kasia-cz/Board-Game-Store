using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.Validation.DataAnnotations.ValidationAttributes
{
    public class NotEmptyListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not ICollection list || list == null || list.Count == 0)
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must contain at least one item.";
        }
    }
}
