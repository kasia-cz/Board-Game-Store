namespace BoardGameStore.Application.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public decimal TotalPrice { get; set; }

        public int UserId { get; set; }

        public List<AddOrderItemDTO> Items { get; set; }
    }
}
