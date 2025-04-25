using BoardGameStore.Domain.Interfaces;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;

namespace BoardGameStore.Domain.Services
{
    public class BoardGameService : IBoardGameService
    {
        private readonly IBoardGameRepository _boardGameRepository;

        public BoardGameService(IBoardGameRepository boardGameRepository)
        {
            _boardGameRepository = boardGameRepository;
        }

        public async Task AddBoardGame(BoardGameModel boardGameModel)
        {
            await _boardGameRepository.AddBoardGame(boardGameModel);
        }

        public async Task DeleteBoardGame(int id)
        {
            await _boardGameRepository.DeleteBoardGame(id);
        }

        public async Task<List<BoardGameModel>> GetAllBoardGames()
        {
            return await _boardGameRepository.GetAllBoardGames();
        }

        public async Task<BoardGameModel> GetBoardGameById(int id)
        {
            return await _boardGameRepository.GetBoardGameById(id);
        }

        public async Task UpdateBoardGame(int id, BoardGameModel boardGameModel)
        {
            await _boardGameRepository.UpdateBoardGame(id, boardGameModel);
        }
    }
}
