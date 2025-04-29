using BoardGameStore.Domain.Interfaces;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;

namespace BoardGameStore.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUser(UserModel userModel)
        {
            await _userRepository.AddUser(userModel);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        public async Task UpdateUser(int id, UserModel userModel)
        {
            await _userRepository.UpdateUser(id, userModel);
        }
    }
}
