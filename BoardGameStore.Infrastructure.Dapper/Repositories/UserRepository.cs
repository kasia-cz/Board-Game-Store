using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task AddUser(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(int id, UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
