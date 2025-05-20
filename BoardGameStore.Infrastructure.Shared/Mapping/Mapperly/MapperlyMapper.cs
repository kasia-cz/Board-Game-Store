using BoardGameStore.Domain.Models;
using BoardGameStore.Infrastructure.Shared.Entities;
using Riok.Mapperly.Abstractions;

namespace BoardGameStore.Infrastructure.Shared.Mapping.Mapperly
{
    [Mapper]
    public partial class MapperlyMapper : IMapper
    {
        // map board game
        public partial BoardGameModel MapBoardGameEntityToModel(BoardGame boardGame);

        public partial BoardGame MapBoardGameModelToEntity(BoardGameModel boardGameModel);

        // map order
        public partial OrderModel MapOrderEntityToModel(Order order);

        public partial Order MapOrderModelToEntity(OrderModel orderModel);

        private partial List<OrderItemModel>? MapOrderItems(List<OrderItem>? items);

        // map user
        public partial UserModel MapUserEntityToModel(User user);

        public partial User MapUserModelToEntity(UserModel userModel);

        private partial AddressModel? MapAddress(Address? address);
    }
}
