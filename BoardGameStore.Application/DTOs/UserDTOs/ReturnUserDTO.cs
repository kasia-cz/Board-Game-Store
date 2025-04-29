namespace BoardGameStore.Application.DTOs.UserDTOs
{
    public class ReturnUserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ReturnAddressDTO? Address { get; set; }
    }
}
