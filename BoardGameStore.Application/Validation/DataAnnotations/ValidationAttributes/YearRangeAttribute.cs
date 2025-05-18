using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.Validation.DataAnnotations.ValidationAttributes
{
    public class YearRangeAttribute : ValidationAttribute
    {
        private readonly int _minYear;

        public YearRangeAttribute(int minYear)
        {
            _minYear = minYear;
        }

        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                return year >= _minYear && year <= DateTime.Now.Year;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between {_minYear} and current year.";
        }
    }
}
