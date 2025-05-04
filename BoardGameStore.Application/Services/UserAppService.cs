using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Mapping;
using BoardGameStore.Domain.Interfaces;

namespace BoardGameStore.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAppService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task AddUser(AddUserDTO addUserDTO)
        {
            var userModel = _mapper.MapAddUserDtoToModel(addUserDTO);
            await _userService.AddUser(userModel);
        }

        public async Task DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
        }

        public async Task<List<ReturnUserShortDTO>> GetAllUsers()
        {
            var userModels = await _userService.GetAllUsers();

            return userModels.Select(_mapper.MapUserModelToReturnUserShortDTO).ToList();
        }

        public async Task<ReturnUserDTO> GetUserById(int id)
        {
            var userModel = await _userService.GetUserById(id);

            return _mapper.MapUserModelToReturnUserDTO(userModel);
        }

        public async Task UpdateUser(int id, AddUserDTO addUserDTO)
        {
            var userModel = _mapper.MapAddUserDtoToModel(addUserDTO);
            await _userService.UpdateUser(id, userModel);
        }
    }
}
