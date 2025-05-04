namespace BoardGameStore.Infrastructure.Dapper.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int BoardGameId { get; set; }
        public BoardGame BoardGame { get; set; }

        public int OrderId { get; set; }
    }
}
