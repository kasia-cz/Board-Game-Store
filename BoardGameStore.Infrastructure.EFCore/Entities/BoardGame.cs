namespace BoardGameStore.Infrastructure.EFCore.Entities
{
    public class BoardGame
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int MinPlayers { get; set; }

        public int MaxPlayers { get; set; }

        public Difficulty Difficulty { get; set; }

        public int AvailableQuantity { get; set; }

        public decimal Price { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}
