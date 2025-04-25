using BoardGameStore.Domain.Enums;

namespace BoardGameStore.Infrastructure.EFCore.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus Status {  get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
