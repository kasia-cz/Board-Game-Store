using AutoMapper;
using BoardGameStore.Domain.Models;
using BoardGameStore.Infrastructure.Shared.Entities;

namespace BoardGameStore.Infrastructure.Shared.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Address, AddressModel>().ReverseMap();

            CreateMap<BoardGame, BoardGameModel>().ReverseMap();

            CreateMap<Order, OrderModel>().ReverseMap();

            CreateMap<OrderItem, OrderItemModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
