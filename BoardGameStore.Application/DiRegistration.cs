using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Application
{
    public static class DiRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddScoped<IBoardGameAppService, BoardGameAppService>();
            return collection;
        }
    }
}
