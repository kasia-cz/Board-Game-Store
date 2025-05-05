using BoardGameStore.Domain.Models;
using BoardGameStore.Infrastructure.Shared.Entities;

namespace BoardGameStore.Infrastructure.Shared.Mapping
{
    public interface IMapper
    {
        // map board game
        BoardGame MapBoardGameModelToEntity(BoardGameModel boardGameModel);

        BoardGameModel MapBoardGameEntityToModel(BoardGame boardGame);

        // map order
        Order MapOrderModelToEntity(OrderModel orderModel);

        OrderModel MapOrderEntityToModel(Order order);

        // map user
        User MapUserModelToEntity(UserModel userModel);

        UserModel MapUserEntityToModel(User user);
    }
}
