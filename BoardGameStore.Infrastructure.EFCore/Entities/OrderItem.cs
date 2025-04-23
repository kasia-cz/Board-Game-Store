namespace BoardGameStore.Infrastructure.EFCore.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
