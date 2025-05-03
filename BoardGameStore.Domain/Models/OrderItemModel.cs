namespace BoardGameStore.Domain.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int BoardGameId { get; set; }

        public BoardGameModel? BoardGame { get; set; }
    }
}
