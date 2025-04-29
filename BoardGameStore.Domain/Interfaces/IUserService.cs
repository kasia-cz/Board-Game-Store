using BoardGameStore.Domain.Models;

namespace BoardGameStore.Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();

        Task<UserModel> GetUserById(int id);

        Task AddUser(UserModel userModel);

        Task UpdateUser(int id, UserModel userModel);

        Task DeleteUser(int id);
    }
}
