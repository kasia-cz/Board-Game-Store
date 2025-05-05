using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Shared.Entities;
using BoardGameStore.Infrastructure.Shared.Mapping;
using Dapper;
using System.Data;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public BoardGameRepository(IDbConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }

        public async Task AddBoardGame(BoardGameModel model)
        {
            var boardGame = _mapper.MapBoardGameModelToEntity(model);

            const string sql = @"
            INSERT INTO BoardGames (Name, Year, MinPlayers, MaxPlayers, Difficulty, AvailableQuantity, Price)
            VALUES (@Name, @Year, @MinPlayers, @MaxPlayers, @Difficulty, @AvailableQuantity, @Price);";

            await _connection.ExecuteAsync(sql, boardGame);
        }

        public async Task DeleteBoardGame(int id)
        {
            const string sql = "DELETE FROM BoardGames WHERE Id = @Id;";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<BoardGameModel>> GetAllBoardGames()
        {
            const string sql = "SELECT * FROM BoardGames;";
            var boardGames = await _connection.QueryAsync<BoardGame>(sql);

            return boardGames.Select(_mapper.MapBoardGameEntityToModel).ToList();
        }

        public async Task<BoardGameModel> GetBoardGameById(int id)
        {
            const string sql = "SELECT * FROM BoardGames WHERE Id = @Id;";
            var boardGame = await _connection.QuerySingleOrDefaultAsync<BoardGame>(sql, new { Id = id });

            return _mapper.MapBoardGameEntityToModel(boardGame);
        }

        public async Task UpdateBoardGame(int id, BoardGameModel model)
        {
            var boardGame = _mapper.MapBoardGameModelToEntity(model);
            boardGame.Id = id;

            const string sql = @"
            UPDATE BoardGames
            SET Name = @Name,
                Year = @Year,
                MinPlayers = @MinPlayers,
                MaxPlayers = @MaxPlayers,
                Difficulty = @Difficulty,
                AvailableQuantity = @AvailableQuantity,
                Price = @Price
            WHERE Id = @Id;";

            await _connection.ExecuteAsync(sql, boardGame);
        }
    }
}
