using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.DTOs.OrderDTOs
{
    public class AddOrderItemDTO
    {
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Board game ID must be greater than 0.")]
        public int BoardGameId { get; set; }
    }
}
