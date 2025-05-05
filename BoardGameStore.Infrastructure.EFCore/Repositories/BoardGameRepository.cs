using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Shared.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly DbContextEFCore _context;
        private readonly IMapper _mapper;

        public BoardGameRepository(DbContextEFCore context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddBoardGame(BoardGameModel boardGameModel)
        {
            var boardGame = _mapper.MapBoardGameModelToEntity(boardGameModel);
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

            return boardGames.Select(_mapper.MapBoardGameEntityToModel).ToList();
        }

        public async Task<BoardGameModel> GetBoardGameById(int id)
        {
            var boardGame = await _context.BoardGames.FindAsync(id);

            return _mapper.MapBoardGameEntityToModel(boardGame);
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
    }
}
