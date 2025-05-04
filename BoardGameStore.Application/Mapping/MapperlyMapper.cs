using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Mapping
{
    public class MapperlyMapper : IMapper
    {
        // map board game
        public BoardGameModel MapAddBoardGameDtoToModel(AddBoardGameDTO addBoardGameDTO)
        {
            throw new NotImplementedException();
        }

        public ReturnBoardGameDTO MapBoardGameModelToReturnBoardGameDTO(BoardGameModel boardGameModel)
        {
            throw new NotImplementedException();
        }

        public ReturnBoardGameShortDTO MapBoardGameModelToReturnBoardGameShortDTO(BoardGameModel boardGameModel)
        {
            throw new NotImplementedException();
        }

        // map order
        public OrderModel MapAddOrderDtoToModel(AddOrderDTO addOrderDTO)
        {
            throw new NotImplementedException();
        }

        public ReturnOrderDTO MapOrderModelToReturnOrderDTO(OrderModel orderModel)
        {
            throw new NotImplementedException();
        }

        public ReturnOrderShortDTO MapOrderModelToReturnOrderShortDTO(OrderModel orderModel)
        {
            throw new NotImplementedException();
        }

        // map user
        public UserModel MapAddUserDtoToModel(AddUserDTO addUserDTO)
        {
            throw new NotImplementedException();
        }

        public ReturnUserDTO MapUserModelToReturnUserDTO(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public ReturnUserShortDTO MapUserModelToReturnUserShortDTO(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
