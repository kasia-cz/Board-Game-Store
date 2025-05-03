using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameStore.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder(AddOrderDTO addOrderDTO)
        {
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
