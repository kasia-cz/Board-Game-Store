using BoardGameStore.Domain.Models;

namespace BoardGameStore.Domain.Interfaces
{
    public interface IBoardGameService
    {
        Task<List<BoardGameModel>> GetAllBoardGames();

        Task<BoardGameModel> GetBoardGameById(int id);

        Task AddBoardGame(BoardGameModel boardGameModel);

        Task UpdateBoardGame(int id, BoardGameModel boardGameModel);

        Task DeleteBoardGame(int id);
    }
}
