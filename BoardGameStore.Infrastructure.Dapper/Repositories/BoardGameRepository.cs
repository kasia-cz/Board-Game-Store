using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Shared.Entities;
using Dapper;
using System.Data;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class BoardGameRepository : IBoardGameRepository
    {
        private readonly IDbConnection _connection;

        public BoardGameRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddBoardGame(BoardGameModel model)
        {
            var boardGame = MapBoardGameModelToEntity(model);

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

            return boardGames.Select(MapBoardGameEntityToModel).ToList();
        }

        public async Task<BoardGameModel> GetBoardGameById(int id)
        {
            const string sql = "SELECT * FROM BoardGames WHERE Id = @Id;";
            var boardGame = await _connection.QuerySingleOrDefaultAsync<BoardGame>(sql, new { Id = id });

            return MapBoardGameEntityToModel(boardGame);
        }

        public async Task UpdateBoardGame(int id, BoardGameModel model)
        {
            var boardGame = MapBoardGameModelToEntity(model);
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
