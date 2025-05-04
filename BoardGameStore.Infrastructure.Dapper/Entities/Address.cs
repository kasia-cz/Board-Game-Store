namespace BoardGameStore.Infrastructure.Dapper.Entities
{
    public class Address
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string AddressLine { get; set; }

        public string PostalCode { get; set; }

        public int UserId { get; set; }
    }
}
