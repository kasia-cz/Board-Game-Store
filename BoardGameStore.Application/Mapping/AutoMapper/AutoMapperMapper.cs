using IAutoMapper = AutoMapper.IMapper;
using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Mapping.AutoMapper
{
    public class AutoMapperMapper : IMapper
    {
        private readonly IAutoMapper _autoMapper;

        public AutoMapperMapper(IAutoMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        // map board game
        public BoardGameModel MapAddBoardGameDtoToModel(AddBoardGameDTO addBoardGameDTO)
        {
            return _autoMapper.Map<BoardGameModel>(addBoardGameDTO);
        }

        public ReturnBoardGameDTO MapBoardGameModelToReturnBoardGameDTO(BoardGameModel boardGameModel)
        {
            return _autoMapper.Map<ReturnBoardGameDTO>(boardGameModel);
        }

        public ReturnBoardGameShortDTO MapBoardGameModelToReturnBoardGameShortDTO(BoardGameModel boardGameModel)
        {
            return _autoMapper.Map<ReturnBoardGameShortDTO>(boardGameModel);
        }

        // map order
        public OrderModel MapAddOrderDtoToModel(AddOrderDTO addOrderDTO)
        {
            return _autoMapper.Map<OrderModel>(addOrderDTO);
        }

        public ReturnOrderDTO MapOrderModelToReturnOrderDTO(OrderModel orderModel)
        {
            return _autoMapper.Map<ReturnOrderDTO>(orderModel);
        }

        public ReturnOrderShortDTO MapOrderModelToReturnOrderShortDTO(OrderModel orderModel)
        {
            return _autoMapper.Map<ReturnOrderShortDTO>(orderModel);
        }

        // map user
        public UserModel MapAddUserDtoToModel(AddUserDTO addUserDTO)
        {
            return _autoMapper.Map<UserModel>(addUserDTO);
        }

        public ReturnUserDTO MapUserModelToReturnUserDTO(UserModel userModel)
        {
            return _autoMapper.Map<ReturnUserDTO>(userModel);
        }

        public ReturnUserShortDTO MapUserModelToReturnUserShortDTO(UserModel userModel)
        {
            return _autoMapper.Map<ReturnUserShortDTO>(userModel);
        }
    }
}