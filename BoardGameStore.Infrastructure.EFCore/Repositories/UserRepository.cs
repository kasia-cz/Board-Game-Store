using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Shared.Mapping;
using BoardGameStore.Infrastructure.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoardGameStore.Infrastructure.EFCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContextEFCore _context;
        private readonly IMapper _mapper;

        public UserRepository(DbContextEFCore context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddUser(UserModel userModel)
        {
            var user = _mapper.MapUserModelToEntity(userModel);
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

            return users.Select(_mapper.MapUserEntityToModel).ToList();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.MapUserEntityToModel(user);
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
    }
}
