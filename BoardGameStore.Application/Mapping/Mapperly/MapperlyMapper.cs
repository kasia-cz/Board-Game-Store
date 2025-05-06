using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace BoardGameStore.Application.Mapping.Mapperly
{
    [Mapper]
    public partial class MapperlyMapper : IMapper
    {
        // map board game
        public partial BoardGameModel MapAddBoardGameDtoToModel(AddBoardGameDTO addBoardGameDTO);

        [MapPropertyFromSource(nameof(ReturnBoardGameDTO.PlayersNumber), Use = nameof(MapPlayersNumber))]
        [MapProperty(nameof(BoardGameModel.AvailableQuantity), nameof(ReturnBoardGameDTO.IsAvailable), Use = nameof(MapAvailability))]
        public partial ReturnBoardGameDTO MapBoardGameModelToReturnBoardGameDTO(BoardGameModel boardGameModel);

        public partial ReturnBoardGameShortDTO MapBoardGameModelToReturnBoardGameShortDTO(BoardGameModel boardGameModel);

        private string MapPlayersNumber(BoardGameModel boardGameModel) => $"{boardGameModel.MinPlayers}-{boardGameModel.MaxPlayers}";

        [UserMapping(Default = false)]
        private bool MapAvailability(int availableQuantity) => availableQuantity > 0;

        // map order
        public partial OrderModel MapAddOrderDtoToModel(AddOrderDTO addOrderDTO);

        public partial ReturnOrderDTO MapOrderModelToReturnOrderDTO(OrderModel orderModel);

        public partial ReturnOrderShortDTO MapOrderModelToReturnOrderShortDTO(OrderModel orderModel);

        // map user
        public partial UserModel MapAddUserDtoToModel(AddUserDTO addUserDTO);

        public partial ReturnUserDTO MapUserModelToReturnUserDTO(UserModel userModel);

        [MapPropertyFromSource(nameof(ReturnUserShortDTO.Name), Use = nameof(MapUserName))]
        public partial ReturnUserShortDTO MapUserModelToReturnUserShortDTO(UserModel userModel);

        private string MapUserName(UserModel userModel) => $"{userModel.FirstName} {userModel.LastName}";
    }
}
