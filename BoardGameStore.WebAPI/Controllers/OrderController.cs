using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IValidationService<AddOrderDTO> _validationService;

        public OrderController(IOrderAppService orderAppService, IValidationService<AddOrderDTO> validationService)
        {
            _orderAppService = orderAppService;
            _validationService = validationService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(AddOrderDTO addOrderDTO)
        {
            _validationService.ValidateAndThrow(addOrderDTO);

            await _orderAppService.AddOrder(addOrderDTO);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<ReturnOrderShortDTO>>> GetAllOrders()
        {
            var result = await _orderAppService.GetAllOrders();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnOrderDTO>> GetOrderById(int id)
        {
            var result = await _orderAppService.GetOrderById(id);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ReturnOrderShortDTO>>> GetUserOrders(int userId)
        {
            var result = await _orderAppService.GetUserOrders(userId);
            return Ok(result);
        }
    }
}
