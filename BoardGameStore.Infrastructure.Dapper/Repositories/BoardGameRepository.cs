using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        public Task AddBoardGame(BoardGameModel boardGameModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBoardGame(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BoardGameModel>> GetAllBoardGames()
        {
            throw new NotImplementedException();
        }

        public Task<BoardGameModel> GetBoardGameById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBoardGame(int id, BoardGameModel boardGameModel)
        {
            throw new NotImplementedException();
        }
    }
}
