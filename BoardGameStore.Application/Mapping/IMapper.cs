using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Mapping
{
    public interface IMapper
    {
        // map board game
        BoardGameModel MapAddBoardGameDtoToModel(AddBoardGameDTO addBoardGameDTO);

        ReturnBoardGameDTO MapBoardGameModelToReturnBoardGameDTO(BoardGameModel boardGameModel);

        ReturnBoardGameShortDTO MapBoardGameModelToReturnBoardGameShortDTO(BoardGameModel boardGameModel);

        // map order
        OrderModel MapAddOrderDtoToModel(AddOrderDTO addOrderDTO);

        ReturnOrderDTO MapOrderModelToReturnOrderDTO(OrderModel orderModel);

        ReturnOrderShortDTO MapOrderModelToReturnOrderShortDTO(OrderModel orderModel);

        // map user
        UserModel MapAddUserDtoToModel(AddUserDTO addUserDTO);

        ReturnUserDTO MapUserModelToReturnUserDTO(UserModel userModel);

        ReturnUserShortDTO MapUserModelToReturnUserShortDTO(UserModel userModel);
    }
}
