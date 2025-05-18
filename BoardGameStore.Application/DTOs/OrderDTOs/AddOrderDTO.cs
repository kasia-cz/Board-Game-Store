using BoardGameStore.Application.Validation.DataAnnotations.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal TotalPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "User ID must be greater than 0.")]
        public int UserId { get; set; }

        [Required]
        [NotEmptyList]
        public List<AddOrderItemDTO> Items { get; set; }
    }
}
