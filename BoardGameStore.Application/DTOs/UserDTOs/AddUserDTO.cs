using BoardGameStore.Application.Validation.DataAnnotations.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.DTOs.UserDTOs
{
    public class AddUserDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-z\s-']+$", ErrorMessage = "First name can contain only letters, spaces, hyphens and apostrophes.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-z\s-']+$", ErrorMessage = "Last name can contain only letters, spaces, hyphens and apostrophes.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DateOfBirthRange]
        public DateTime DateOfBirth { get; set; }

        public AddAddressDTO? Address { get; set; }
    }
}
