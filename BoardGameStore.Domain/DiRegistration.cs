using BoardGameStore.Domain.Interfaces;
using BoardGameStore.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Domain
{
    public static class DiRegistration
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection collection)
        {
            collection.AddScoped<IBoardGameService, BoardGameService>();
            collection.AddScoped<IUserService, UserService>();
            collection.AddScoped<IOrderService, OrderService>();
            return collection;
        }
    }
}
