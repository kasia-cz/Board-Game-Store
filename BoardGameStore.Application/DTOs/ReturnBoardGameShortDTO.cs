namespace BoardGameStore.Application.DTOs
{
    public class ReturnBoardGameShortDTO
    {
        public string Name { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Price { get; set; }
    }
}
