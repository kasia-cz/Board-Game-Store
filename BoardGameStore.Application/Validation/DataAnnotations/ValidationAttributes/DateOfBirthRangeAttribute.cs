using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.Validation.DataAnnotations.ValidationAttributes
{
    public class DateOfBirthRangeAttribute : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;

        public DateOfBirthRangeAttribute(int minAge = 18, int maxAge = 100)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                var minDate = DateTime.Today.AddYears(-_maxAge);
                var maxDate = DateTime.Today.AddYears(-_minAge);

                return date >= minDate && date <= maxDate;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be between {DateTime.Today.AddYears(-_maxAge):dd-MM-yyyy} and {DateTime.Today.AddYears(-_minAge):dd-MM-yyyy}.";
        }
    }
}
