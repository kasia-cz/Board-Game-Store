using BoardGameStore.Application.DTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Domain.Interfaces;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Services
{
    public class BoardGameAppService : IBoardGameAppService
    {
        private readonly IBoardGameService _boardGameService;
        // todo: add mappers

        public BoardGameAppService(IBoardGameService boardGameService)
        {
            _boardGameService = boardGameService;
        }

        public async Task AddBoardGame(AddBoardGameDTO addBoardGameDTO)
        {
            var boardGameModel = new BoardGameModel
            {
                Name = addBoardGameDTO.Name,
                Year = addBoardGameDTO.Year,
                MinPlayers = addBoardGameDTO.MinPlayers,
                MaxPlayers = addBoardGameDTO.MaxPlayers,
                Difficulty = addBoardGameDTO.Difficulty,
                AvailableQuantity = addBoardGameDTO.AvailableQuantity,
                Price = addBoardGameDTO.Price
            };

            await _boardGameService.AddBoardGame(boardGameModel);
        }

        public async Task DeleteBoardGame(int id)
        {
            await _boardGameService.DeleteBoardGame(id);
        }

        public async Task<List<ReturnBoardGameShortDTO>> GetAllBoardGames()
        {
            var boardGameModels = await _boardGameService.GetAllBoardGames();
            var boardGames = boardGameModels.Select(boardGameModel => new ReturnBoardGameShortDTO
            {
                Name = boardGameModel.Name,
                IsAvailable = boardGameModel.AvailableQuantity > 0,
                Price = boardGameModel.Price
            }).ToList();

            return boardGames;
        }

        public async Task<ReturnBoardGameDTO> GetBoardGameById(int id)
        {
            var boardGameModel = await _boardGameService.GetBoardGameById(id);

            var returnBoardGameDTO = new ReturnBoardGameDTO
            {
                Name = boardGameModel.Name,
                Year = boardGameModel.Year,
                PlayersNumber = $"{boardGameModel.MinPlayers}-{boardGameModel.MaxPlayers}",
                Difficulty = boardGameModel.Difficulty.ToString(),
                IsAvailable = boardGameModel.AvailableQuantity > 0,
                Price = boardGameModel.Price
            };

            return returnBoardGameDTO;
        }

        public async Task UpdateBoardGame(int id, AddBoardGameDTO addBoardGameDTO)
        {
            var boardGameModel = new BoardGameModel
            {
                Name = addBoardGameDTO.Name,
                Year = addBoardGameDTO.Year,
                MinPlayers = addBoardGameDTO.MinPlayers,
                MaxPlayers = addBoardGameDTO.MaxPlayers,
                Difficulty = addBoardGameDTO.Difficulty,
                AvailableQuantity = addBoardGameDTO.AvailableQuantity,
                Price = addBoardGameDTO.Price
            };

            await _boardGameService.UpdateBoardGame(id, boardGameModel);
        }
    }
}
