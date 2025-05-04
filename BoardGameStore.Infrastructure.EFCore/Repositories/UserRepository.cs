using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextEFCore _context;

        public UserRepository(DbContextEFCore context)
        {
            _context = context;
        }

        public async Task AddUser(UserModel userModel)
        {
            var user = MapUserModelToEntity(userModel);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(MapUserEntityToModel).ToList();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);

            return MapUserEntityToModel(user);
        }

        public async Task UpdateUser(int id, UserModel userModel)
        {
            var userToUpdate = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);

            userToUpdate.FirstName = userModel.FirstName;
            userToUpdate.LastName = userModel.LastName;
            userToUpdate.Email = userModel.Email;
            userToUpdate.PhoneNumber = userModel.PhoneNumber;
            userToUpdate.DateOfBirth = userModel.DateOfBirth;

            if (userModel.Address != null)
            {
                userToUpdate.Address ??= new Address();

                userToUpdate.Address.City = userModel.Address.City;
                userToUpdate.Address.AddressLine = userModel.Address.AddressLine;
                userToUpdate.Address.PostalCode = userModel.Address.PostalCode;
            }
            else
            {
                userToUpdate.Address = null;
            }

            await _context.SaveChangesAsync();
        }

        private static User MapUserModelToEntity(UserModel userModel)
        {
            var user = new User
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                DateOfBirth = userModel.DateOfBirth,
            };

            if (userModel.Address != null)
            {
                user.Address = new Address
                {
                    City = userModel.Address.City,
                    AddressLine = userModel.Address.AddressLine,
                    PostalCode = userModel.Address.PostalCode,
                };
            }

            return user;
        }

        private static UserModel MapUserEntityToModel(User user)
        {
            var userModel = new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
            };

            if (user.Address != null)
            {
                userModel.Address = new AddressModel
                {
                    Id = user.Address.Id,
                    City = user.Address.City,
                    AddressLine = user.Address.AddressLine,
                    PostalCode = user.Address.PostalCode,
                };
            }

            return userModel;
        }
    }
}
