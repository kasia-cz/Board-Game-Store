using IAutoMapper = AutoMapper.IMapper;
using BoardGameStore.Domain.Models;
using BoardGameStore.Infrastructure.Shared.Entities;

namespace BoardGameStore.Infrastructure.Shared.Mapping.AutoMapper
{
    public class AutoMapperMapper : IMapper
    {
        private readonly IAutoMapper _autoMapper;

        public AutoMapperMapper(IAutoMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        // map board game
        public BoardGameModel MapBoardGameEntityToModel(BoardGame boardGame)
        {
            return _autoMapper.Map<BoardGameModel>(boardGame);
        }

        public BoardGame MapBoardGameModelToEntity(BoardGameModel boardGameModel)
        {
            return _autoMapper.Map<BoardGame>(boardGameModel);
        }

        // map order
        public OrderModel MapOrderEntityToModel(Order order)
        {
            return _autoMapper.Map<OrderModel>(order);
        }

        public Order MapOrderModelToEntity(OrderModel orderModel)
        {
            return _autoMapper.Map<Order>(orderModel);
        }

        // map user
        public UserModel MapUserEntityToModel(User user)
        {
            return _autoMapper.Map<UserModel>(user);
        }

        public User MapUserModelToEntity(UserModel userModel)
        {
            return _autoMapper.Map<User>(userModel);
        }
    }
}
