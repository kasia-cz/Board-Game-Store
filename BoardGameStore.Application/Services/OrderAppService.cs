using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Mapping;
using BoardGameStore.Domain.Interfaces;

namespace BoardGameStore.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderAppService(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task AddOrder(AddOrderDTO addOrderDTO)
        {
            var orderModel = _mapper.MapAddOrderDtoToModel(addOrderDTO);
            await _orderService.AddOrder(orderModel);
        }

        public async Task<List<ReturnOrderShortDTO>> GetAllOrders()
        {
            var orderModels = await _orderService.GetAllOrders();

            return orderModels.Select(_mapper.MapOrderModelToReturnOrderShortDTO).ToList();
        }

        public async Task<ReturnOrderDTO> GetOrderById(int id)
        {
            var orderModel = await _orderService.GetOrderById(id);

            return _mapper.MapOrderModelToReturnOrderDTO(orderModel);
        }

        public async Task<List<ReturnOrderShortDTO>> GetUserOrders(int userId)
        {
            var orderModels = await _orderService.GetUserOrders(userId);

            return orderModels.Select(_mapper.MapOrderModelToReturnOrderShortDTO).ToList();
        }
    }
}
