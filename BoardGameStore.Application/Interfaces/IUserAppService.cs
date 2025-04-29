using BoardGameStore.Application.DTOs.UserDTOs;

namespace BoardGameStore.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<List<ReturnUserShortDTO>> GetAllUsers();

        Task<ReturnUserDTO> GetUserById(int id);

        Task AddUser(AddUserDTO addUserDTO);

        Task UpdateUser(int id, AddUserDTO addUserDTO);

        Task DeleteUser(int id);
    }
}
