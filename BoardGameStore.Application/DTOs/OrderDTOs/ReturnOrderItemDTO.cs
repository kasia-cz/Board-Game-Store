using BoardGameStore.Application.DTOs.BoardGameDTOs;

namespace BoardGameStore.Application.DTOs.OrderDTOs
{
    public class ReturnOrderItemDTO
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ReturnBoardGameShortDTO BoardGame { get; set; }
    }
}
