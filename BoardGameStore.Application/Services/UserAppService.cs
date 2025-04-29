using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Domain.Interfaces;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;

        public UserAppService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task AddUser(AddUserDTO addUserDTO)
        {
            var userModel = MapAddUserDtoToModel(addUserDTO);
            await _userService.AddUser(userModel);
        }

        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }

        public async Task<List<ReturnUserShortDTO>> GetAllUsers()
        {
            var userModels = await _userService.GetAllUsers();

            return userModels.Select(MapUserModelToReturnUserShortDTO).ToList();
        }

        public async Task<ReturnUserDTO> GetUserById(int id)
        {
            var userModel = await _userService.GetUserById(id);

            return MapUserModelToReturnUserDTO(userModel);
        }

        public async Task UpdateUser(int id, AddUserDTO addUserDTO)
        {
            var userModel = MapAddUserDtoToModel(addUserDTO);
            await _userService.UpdateUser(id, userModel);
        }

        private static UserModel MapAddUserDtoToModel(AddUserDTO addUserDTO)
        {
            var userModel = new UserModel
            {
                FirstName = addUserDTO.FirstName,
                LastName = addUserDTO.LastName,
                Email = addUserDTO.Email,
                PhoneNumber = addUserDTO.PhoneNumber,
                DateOfBirth = addUserDTO.DateOfBirth,
            };

            if (addUserDTO.Address != null)
            {
                userModel.Address = new AddressModel
                {
                    City = addUserDTO.Address.City,
                    AddressLine = addUserDTO.Address.AddressLine,
                    PostalCode = addUserDTO.Address.PostalCode,
                };
            }

            return userModel;
        }

        private static ReturnUserDTO MapUserModelToReturnUserDTO(UserModel userModel)
        {
            var userDTO = new ReturnUserDTO
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                DateOfBirth = userModel.DateOfBirth,
            };

            if (userModel.Address != null)
            {
                userDTO.Address = new ReturnAddressDTO
                {
                    Id = userModel.Address.Id,
                    City = userModel.Address.City,
                    AddressLine = userModel.Address.AddressLine,
                    PostalCode = userModel.Address.PostalCode,
                };
            }

            return userDTO;
        }

        private static ReturnUserShortDTO MapUserModelToReturnUserShortDTO(UserModel userModel)
        {
            return new ReturnUserShortDTO
            {
                Id = userModel.Id,
                Name = $"{userModel.FirstName} {userModel.LastName}",
                Email = userModel.Email
            };
        }
    }
}
