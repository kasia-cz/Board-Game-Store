namespace BoardGameStore.Application.DTOs.UserDTOs
{
    public class AddUserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public AddAddressDTO? Address { get; set; }
    }
}
