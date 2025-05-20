using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardGameController : ControllerBase
    {
        private readonly IBoardGameAppService _boardGameAppService;
        private readonly IValidationService<AddBoardGameDTO> _validationService;

        public BoardGameController(IBoardGameAppService boardGameAppService, IValidationService<AddBoardGameDTO> validationService)
        {
            _boardGameAppService = boardGameAppService;
            _validationService = validationService;
        }

        [HttpPost]
        public async Task<ActionResult> AddBoardGame(AddBoardGameDTO addBoardGameDTO)
        {
            _validationService.ValidateAndThrow(addBoardGameDTO);

            await _boardGameAppService.AddBoardGame(addBoardGameDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoardGame(int id)
        {
            await _boardGameAppService.DeleteBoardGame(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<ReturnBoardGameShortDTO>>> GetAllBoardGames()
        {
            var result = await _boardGameAppService.GetAllBoardGames();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnBoardGameDTO>> GetBoardGameById(int id)
        {
            var result = await _boardGameAppService.GetBoardGameById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBoardGame(int id, AddBoardGameDTO addBoardGameDTO)
        {
            _validationService.ValidateAndThrow(addBoardGameDTO);

            await _boardGameAppService.UpdateBoardGame(id, addBoardGameDTO);
            return NoContent();
        }
    }
}
