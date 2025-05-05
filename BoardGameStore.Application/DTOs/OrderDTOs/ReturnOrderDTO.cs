namespace BoardGameStore.Application.DTOs.OrderDTOs
{
    public class ReturnOrderDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        public List<ReturnOrderItemDTO> Items { get; set; }
    }
}
