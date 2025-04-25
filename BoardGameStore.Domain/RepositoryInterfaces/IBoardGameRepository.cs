using BoardGameStore.Domain.Models;

namespace BoardGameStore.Domain.RepositoryInterfaces
{
    public interface IBoardGameRepository
    {
        Task<List<BoardGameModel>> GetAllBoardGames();

        Task<BoardGameModel> GetBoardGameById(int id);

        Task AddBoardGame(BoardGameModel boardGameModel);

        Task UpdateBoardGame(int id, BoardGameModel boardGameModel);

        Task DeleteBoardGame(int id);
    }
}
