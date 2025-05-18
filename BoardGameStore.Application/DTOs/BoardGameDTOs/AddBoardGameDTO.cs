using BoardGameStore.Application.Validation.DataAnnotations.ValidationAttributes;
using BoardGameStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Application.DTOs.BoardGameDTOs
{
    public class AddBoardGameDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [YearRange(1900)]
        public int Year { get; set; }

        [Range(1, 50)]
        public int MinPlayers { get; set; }

        [Range(1, 50)]
        [MaxPlayersGreaterThanMin(nameof(MinPlayers))]
        public int MaxPlayers { get; set; }

        [Required]
        [EnumDataType(typeof(GameDifficulty))]
        public GameDifficulty Difficulty { get; set; }

        [Range(0, 1000)]
        public int AvailableQuantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}
