using BoardGameStore.Application.DTOs.OrderDTOs;

namespace BoardGameStore.Application.Interfaces
{
    public interface IOrderAppService
    {
        Task<List<ReturnOrderShortDTO>> GetAllOrders();

        Task<List<ReturnOrderShortDTO>> GetUserOrders(int userId);

        Task<ReturnOrderDTO> GetOrderById(int id);

        Task AddOrder(AddOrderDTO addOrderDTO);
    }
}
