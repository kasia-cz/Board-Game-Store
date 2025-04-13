using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.EFCore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Infrastructure.EFCore
{
    public static class DiRegistration
    {
        public static IServiceCollection AddEFCoreRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IBoardGameRepository, BoardGameRepository>();
            return collection;
        }
    }
}
