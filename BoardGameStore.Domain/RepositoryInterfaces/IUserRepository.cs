using BoardGameStore.Domain.Models;

namespace BoardGameStore.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();

        Task<UserModel> GetUserById(int id);

        Task AddUser(UserModel userModel);

        Task UpdateUser(int id, UserModel userModel);

        Task DeleteUser(int id);
    }
}
