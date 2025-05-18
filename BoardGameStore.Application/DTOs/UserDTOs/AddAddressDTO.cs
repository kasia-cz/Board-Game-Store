using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.DTOs.UserDTOs
{
    public class AddAddressDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-z\s-]+$", ErrorMessage = "City can contain only letters, spaces and hyphens.")]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Za-z\d\s-./]+$", ErrorMessage = "Address line can contain only letters, numbers, spaces, hyphens, dots and slashes.")]
        public string AddressLine { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Invalid postal code format. Valid format: xx-yyy.")]
        public string PostalCode { get; set; }
    }
}
