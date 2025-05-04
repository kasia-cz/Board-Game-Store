using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Mapping;
using BoardGameStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Application
{
    public static class DiRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddScoped<IBoardGameAppService, BoardGameAppService>();
            collection.AddScoped<IUserAppService, UserAppService>();
            collection.AddScoped<IOrderAppService, OrderAppService>();
            collection.AddScoped<IMapper, ManualMapper>();
            //collection.AddScoped<IMapper, AutoMapper>();
            //collection.AddScoped<IMapper, MapperlyMapper>();
            return collection;
        }
    }
}
