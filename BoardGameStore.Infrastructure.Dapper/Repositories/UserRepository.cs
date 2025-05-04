using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Dapper.Entities;
using Dapper;
using System.Data;

namespace BoardGameStore.Infrastructure.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddUser(UserModel userModel)
        {
            var user = MapUserModelToEntity(userModel);

            const string userSql = @"
            INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, DateOfBirth)
            VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @DateOfBirth);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            var userId = await _connection.QuerySingleAsync<int>(userSql, user);

            if (user.Address != null)
            {
                user.Address.UserId = userId;

                const string addressSql = @"
                INSERT INTO Addresses (City, AddressLine, PostalCode, UserId)
                VALUES (@City, @AddressLine, @PostalCode, @UserId);";

                await _connection.ExecuteAsync(addressSql, user.Address);
            }
        }

        public async Task DeleteUser(int id)
        {
            const string sql = "DELETE FROM Users WHERE Id = @Id;";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            const string sql = "SELECT * FROM Users;";
            var users = await _connection.QueryAsync<User>(sql);

            return users.Select(MapUserEntityToModel).ToList();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            const string userSql = "SELECT * FROM Users WHERE Id = @Id;";
            const string addressSql = "SELECT * FROM Addresses WHERE UserId = @UserId;";

            var user = await _connection.QuerySingleOrDefaultAsync<User>(userSql, new { Id = id });
            var address = await _connection.QuerySingleOrDefaultAsync<Address>(addressSql, new { UserId = id });
            user.Address = address;

            return MapUserEntityToModel(user);
        }

        public async Task UpdateUser(int id, UserModel userModel)
        {
            var user = MapUserModelToEntity(userModel);
            user.Id = id;

            const string userSql = @"
            UPDATE Users SET
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                PhoneNumber = @PhoneNumber,
                DateOfBirth = @DateOfBirth
            WHERE Id = @Id;";

            await _connection.ExecuteAsync(userSql, user);

            if (user.Address != null)
            {
                user.Address.UserId = id;
                const string hasAddressSql = "SELECT COUNT(1) FROM Addresses WHERE UserId = @UserId;";
                string addressSql;

                if (await _connection.ExecuteScalarAsync<bool>(hasAddressSql, new { UserId = id }))
                {
                    addressSql = @"
                    UPDATE Addresses SET
                        City = @City,
                        AddressLine = @AddressLine,
                        PostalCode = @PostalCode
                    WHERE UserId = @UserId;";
                }
                else
                {
                    addressSql = @"
                    INSERT INTO Addresses (City, AddressLine, PostalCode, UserId)
                    VALUES (@City, @AddressLine, @PostalCode, @UserId);";
                }

                await _connection.ExecuteAsync(addressSql, user.Address);
            }
            else
            {
                const string sqlDelete = "DELETE FROM Addresses WHERE UserId = @UserId;";
                await _connection.ExecuteAsync(sqlDelete, new { UserId = id });
            }
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
