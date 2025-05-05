using BoardGameStore.Domain.Models;
using BoardGameStore.Infrastructure.Shared.Entities;

namespace BoardGameStore.Infrastructure.Shared.Mapping
{
    public class MapperlyMapper : IMapper
    {
        // map board game
        public BoardGameModel MapBoardGameEntityToModel(BoardGame boardGame)
        {
            throw new NotImplementedException();
        }

        public BoardGame MapBoardGameModelToEntity(BoardGameModel boardGameModel)
        {
            throw new NotImplementedException();
        }

        // map order
        public OrderModel MapOrderEntityToModel(Order order)
        {
            throw new NotImplementedException();
        }

        public Order MapOrderModelToEntity(OrderModel orderModel)
        {
            throw new NotImplementedException();
        }

        // map user
        public UserModel MapUserEntityToModel(User user)
        {
            throw new NotImplementedException();
        }

        public User MapUserModelToEntity(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
