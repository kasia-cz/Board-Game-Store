using BoardGameStore.Application.DTOs.BoardGameDTOs;
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
            var boardGameModel = MapAddBoardGameDtoToModel(addBoardGameDTO);
            await _boardGameService.AddBoardGame(boardGameModel);
        }

        public async Task DeleteBoardGame(int id)
        {
            await _boardGameService.DeleteBoardGame(id);
        }

        public async Task<List<ReturnBoardGameShortDTO>> GetAllBoardGames()
        {
            var boardGameModels = await _boardGameService.GetAllBoardGames();

            return boardGameModels.Select(MapBoardGameModelToReturnBoardGameShortDTO).ToList();
        }

        public async Task<ReturnBoardGameDTO> GetBoardGameById(int id)
        {
            var boardGameModel = await _boardGameService.GetBoardGameById(id);

            return MapBoardGameModelToReturnBoardGameDTO(boardGameModel);
        }

        public async Task UpdateBoardGame(int id, AddBoardGameDTO addBoardGameDTO)
        {
            var boardGameModel = MapAddBoardGameDtoToModel(addBoardGameDTO);
            await _boardGameService.UpdateBoardGame(id, boardGameModel);
        }

        private static BoardGameModel MapAddBoardGameDtoToModel(AddBoardGameDTO addBoardGameDTO)
        {
            return new BoardGameModel
            {
                Name = addBoardGameDTO.Name,
                Year = addBoardGameDTO.Year,
                MinPlayers = addBoardGameDTO.MinPlayers,
                MaxPlayers = addBoardGameDTO.MaxPlayers,
                Difficulty = addBoardGameDTO.Difficulty,
                AvailableQuantity = addBoardGameDTO.AvailableQuantity,
                Price = addBoardGameDTO.Price
            };
        }

        private static ReturnBoardGameDTO MapBoardGameModelToReturnBoardGameDTO(BoardGameModel boardGameModel)
        {
            return new ReturnBoardGameDTO
            {
                Id = boardGameModel.Id,
                Name = boardGameModel.Name,
                Year = boardGameModel.Year,
                PlayersNumber = $"{boardGameModel.MinPlayers}-{boardGameModel.MaxPlayers}",
                Difficulty = boardGameModel.Difficulty.ToString(),
                IsAvailable = boardGameModel.AvailableQuantity > 0,
                Price = boardGameModel.Price
            };
        }

        private static ReturnBoardGameShortDTO MapBoardGameModelToReturnBoardGameShortDTO(BoardGameModel boardGameModel)
        {
            return new ReturnBoardGameShortDTO
            {
                Id = boardGameModel.Id,
                Name = boardGameModel.Name,
                Price = boardGameModel.Price
            };
        }
    }
}
