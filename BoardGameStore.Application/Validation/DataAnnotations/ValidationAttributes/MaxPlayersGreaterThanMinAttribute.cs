using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.Validation.DataAnnotations.ValidationAttributes
{
    public class MaxPlayersGreaterThanMinAttribute : ValidationAttribute
    {
        private readonly string _minPlayersPropertyName;

        public MaxPlayersGreaterThanMinAttribute(string minPlayersPropertyName)
        {
            _minPlayersPropertyName = minPlayersPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var maxPlayers = value as int? ?? 0;
            var minProp = validationContext.ObjectType.GetProperty(_minPlayersPropertyName);

            if (minProp == null)
                return new ValidationResult($"Unknown property: {_minPlayersPropertyName}");

            var minPlayers = minProp.GetValue(validationContext.ObjectInstance) as int? ?? 0;

            if (maxPlayers < minPlayers)
            {
                return new ValidationResult("MaxPlayers must be greater than or equal to MinPlayers.");
            }

            return ValidationResult.Success;
        }
    }
}
