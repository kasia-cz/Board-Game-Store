namespace BoardGameStore.Application.DTOs.BoardGameDTOs
{
    public class ReturnBoardGameShortDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Price { get; set; }
    }
}
