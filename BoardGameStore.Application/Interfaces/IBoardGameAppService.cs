using BoardGameStore.Application.DTOs.BoardGameDTOs;

namespace BoardGameStore.Application.Interfaces
{
    public interface IBoardGameAppService
    {
        Task<List<ReturnBoardGameShortDTO>> GetAllBoardGames();

        Task<ReturnBoardGameDTO> GetBoardGameById(int id);

        Task AddBoardGame(AddBoardGameDTO addBoardGameDTO);

        Task UpdateBoardGame(int id, AddBoardGameDTO addBoardGameDTO);

        Task DeleteBoardGame(int id); 
    }
}
