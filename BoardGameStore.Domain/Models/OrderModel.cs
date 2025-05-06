using BoardGameStore.Domain.Enums;

namespace BoardGameStore.Domain.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status { get; set; }

        public int UserId { get; set; }

        public List<OrderItemModel>? Items { get; set; }
    }
}
