using BoardGameStore.Domain.Enums;

namespace BoardGameStore.Infrastructure.Dapper.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status {  get; set; }

        public int UserId { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
