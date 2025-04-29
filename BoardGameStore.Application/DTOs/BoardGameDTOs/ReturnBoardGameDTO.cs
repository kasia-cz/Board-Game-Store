namespace BoardGameStore.Application.DTOs.BoardGameDTOs
{
    public class ReturnBoardGameDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public string PlayersNumber { get; set; }

        public string Difficulty { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Price { get; set; }
    }
}
