using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Infrastructure.Dapper
{
    public static class DiRegistration
    {
        public static IServiceCollection AddDapperRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IBoardGameRepository, BoardGameRepository>();
            return collection;
        }
    }
}
