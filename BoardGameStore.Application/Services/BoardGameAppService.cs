using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Mapping;
using BoardGameStore.Domain.Interfaces;

namespace BoardGameStore.Application.Services
{
    public class BoardGameAppService : IBoardGameAppService
    {
        private readonly IBoardGameService _boardGameService;
        private readonly IMapper _mapper;

        public BoardGameAppService(IBoardGameService boardGameService, IMapper mapper)
        {
            _boardGameService = boardGameService;
            _mapper = mapper;
        }

        public async Task AddBoardGame(AddBoardGameDTO addBoardGameDTO)
        {
            var boardGameModel = _mapper.MapAddBoardGameDtoToModel(addBoardGameDTO);
            await _boardGameService.AddBoardGame(boardGameModel);
        }

        public async Task DeleteBoardGame(int id)
        {
            await _boardGameService.DeleteBoardGame(id);
        }

        public async Task<List<ReturnBoardGameShortDTO>> GetAllBoardGames()
        {
            var boardGameModels = await _boardGameService.GetAllBoardGames();

            return boardGameModels.Select(_mapper.MapBoardGameModelToReturnBoardGameShortDTO).ToList();
        }

        public async Task<ReturnBoardGameDTO> GetBoardGameById(int id)
        {
            var boardGameModel = await _boardGameService.GetBoardGameById(id);

            return _mapper.MapBoardGameModelToReturnBoardGameDTO(boardGameModel);
        }

        public async Task UpdateBoardGame(int id, AddBoardGameDTO addBoardGameDTO)
        {
            var boardGameModel = _mapper.MapAddBoardGameDtoToModel(addBoardGameDTO);
            await _boardGameService.UpdateBoardGame(id, boardGameModel);
        }
    }
}
