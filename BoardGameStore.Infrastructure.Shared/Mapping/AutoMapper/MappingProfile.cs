using AutoMapper;
using BoardGameStore.Domain.Models;
using BoardGameStore.Infrastructure.Shared.Entities;

namespace BoardGameStore.Infrastructure.Shared.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BoardGame, BoardGameModel>().ReverseMap();

            CreateMap<Address, AddressModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<OrderItem, OrderItemModel>().ReverseMap();

            CreateMap<Order, OrderModel>().ReverseMap();
        }
    }
}
