using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly DbContextEFCore _context;
        // todo: add mappers

        public BoardGameRepository(DbContextEFCore context)
        {
            _context = context;
        }

        public async Task AddBoardGame(BoardGameModel boardGameModel)
        {
            var boardGame = MapBoardGameModelToEntity(boardGameModel);
            _context.BoardGames.Add(boardGame);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBoardGame(int id)
        {
            var boardGame = await _context.BoardGames.FindAsync(id);
            if (boardGame != null)
            {
                _context.BoardGames.Remove(boardGame);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<BoardGameModel>> GetAllBoardGames()
        {
            var boardGames = await _context.BoardGames.ToListAsync();

            return boardGames.Select(MapBoardGameEntityToModel).ToList();
        }

        public async Task<BoardGameModel> GetBoardGameById(int id)
        {
            var boardGame = await _context.BoardGames.FindAsync(id);

            return MapBoardGameEntityToModel(boardGame);
        }

        public async Task UpdateBoardGame(int id, BoardGameModel boardGameModel)
        {
            var boardGameToUpdate = await _context.BoardGames.FindAsync(id);

            boardGameToUpdate.Name = boardGameModel.Name;
            boardGameToUpdate.Year = boardGameModel.Year;
            boardGameToUpdate.MinPlayers = boardGameModel.MinPlayers;
            boardGameToUpdate.MaxPlayers = boardGameModel.MaxPlayers;
            boardGameToUpdate.Difficulty = boardGameModel.Difficulty;
            boardGameToUpdate.AvailableQuantity = boardGameModel.AvailableQuantity;
            boardGameToUpdate.Price = boardGameModel.Price;

            await _context.SaveChangesAsync();
        }

        private static BoardGame MapBoardGameModelToEntity(BoardGameModel boardGameModel)
        {
            return new BoardGame
            {
                Name = boardGameModel.Name,
                Year = boardGameModel.Year,
                MinPlayers = boardGameModel.MinPlayers,
                MaxPlayers = boardGameModel.MaxPlayers,
                Difficulty = boardGameModel.Difficulty,
                AvailableQuantity = boardGameModel.AvailableQuantity,
                Price = boardGameModel.Price
            };
        }

        private static BoardGameModel MapBoardGameEntityToModel(BoardGame boardGame)
        {
            return new BoardGameModel
            {
                Id = boardGame.Id,
                Name = boardGame.Name,
                Year = boardGame.Year,
                MinPlayers = boardGame.MinPlayers,
                MaxPlayers = boardGame.MaxPlayers,
                Difficulty = boardGame.Difficulty,
                AvailableQuantity = boardGame.AvailableQuantity,
                Price = boardGame.Price
            };
        }
    }
}
