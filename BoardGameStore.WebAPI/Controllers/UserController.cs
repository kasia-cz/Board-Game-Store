using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        
        [HttpPost]
        public async Task<ActionResult> AddUser(AddUserDTO addUserDTO)
        {
            await _userAppService.AddUser(addUserDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userAppService.DeleteUser(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<ReturnUserShortDTO>>> GetAllUsers()
        {
            var result = await _userAppService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnUserDTO>> GetUserById(int id)
        {
            var result = await _userAppService.GetUserById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, AddUserDTO addUserDTO)
        {
            await _userAppService.UpdateUser(id, addUserDTO);
            return NoContent();
        }
    }
}
