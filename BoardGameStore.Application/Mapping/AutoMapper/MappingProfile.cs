using AutoMapper;
using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Domain.Models;

namespace BoardGameStore.Application.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // map board game
            CreateMap<AddBoardGameDTO, BoardGameModel>();

            CreateMap<BoardGameModel, ReturnBoardGameDTO>()
                .ForMember(dest => dest.PlayersNumber, opt => opt.MapFrom(src => $"{src.MinPlayers}-{src.MaxPlayers}"))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty.ToString()))
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.AvailableQuantity > 0));

            CreateMap<BoardGameModel, ReturnBoardGameShortDTO>();

            // map order
            CreateMap<AddOrderDTO, OrderModel>();

            CreateMap<AddOrderItemDTO, OrderItemModel>();

            CreateMap<OrderModel, ReturnOrderDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<OrderModel, ReturnOrderShortDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<OrderItemModel, ReturnOrderItemDTO>();

            // map user
            CreateMap<AddUserDTO, UserModel>();

            CreateMap<AddAddressDTO, AddressModel>();

            CreateMap<UserModel, ReturnUserDTO>();

            CreateMap<UserModel, ReturnUserShortDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<AddressModel, ReturnAddressDTO>();
        }
    }
}
