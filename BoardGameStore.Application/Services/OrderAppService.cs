using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Domain.Interfaces;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;

        public OrderAppService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task AddOrder(AddOrderDTO addOrderDTO)
        {
            var orderModel = MapAddOrderDtoToModel(addOrderDTO);
            await _orderService.AddOrder(orderModel);
        }

        public async Task<List<ReturnOrderShortDTO>> GetAllOrders()
        {
            var orderModels = await _orderService.GetAllOrders();

            return orderModels.Select(MapOrderModelToReturnOrderShortDTO).ToList();
        }

        public async Task<ReturnOrderDTO> GetOrderById(int id)
        {
            var orderModel = await _orderService.GetOrderById(id);

            return MapOrderModelToReturnOrderDTO(orderModel);
        }

        public async Task<List<ReturnOrderShortDTO>> GetUserOrders(int userId)
        {
            var orderModels = await _orderService.GetUserOrders(userId);

            return orderModels.Select(MapOrderModelToReturnOrderShortDTO).ToList();
        }

        private static OrderModel MapAddOrderDtoToModel(AddOrderDTO addOrderDTO)
        {
            var OrderModel = new OrderModel
            {
                TotalPrice = addOrderDTO.TotalPrice,
                UserId = addOrderDTO.UserId,
                Items = addOrderDTO.Items.Select(orderItem => new OrderItemModel
                {
                    BoardGameId = orderItem.BoardGameId,
                    Quantity = orderItem.Quantity
                }).ToList()
            };

            return OrderModel;
        }

        private static ReturnOrderDTO MapOrderModelToReturnOrderDTO(OrderModel orderModel)
        {
            var OrderDTO = new ReturnOrderDTO
            {
                Id = orderModel.Id,
                Date = orderModel.Date,
                TotalPrice = orderModel.TotalPrice,
                Status = orderModel.Status,
                UserId = orderModel.UserId,
                Items = orderModel.Items.Select(orderItem => new ReturnOrderItemDTO
                {
                    Id = orderItem.Id,
                    Quantity = orderItem.Quantity,
                    BoardGame = new ReturnBoardGameShortDTO
                    {
                        Id = orderItem.BoardGame.Id,
                        Name = orderItem.BoardGame.Name,
                        Price = orderItem.BoardGame.Price
                    }
                }).ToList()
            };

            return OrderDTO;
        }

        private static ReturnOrderShortDTO MapOrderModelToReturnOrderShortDTO(OrderModel orderModel)
        {
            return new ReturnOrderShortDTO
            {
                Id = orderModel.Id,
                Date = orderModel.Date,
                TotalPrice = orderModel.TotalPrice,
                Status = orderModel.Status,
                UserId = orderModel.UserId
            };
        }
    }
}
