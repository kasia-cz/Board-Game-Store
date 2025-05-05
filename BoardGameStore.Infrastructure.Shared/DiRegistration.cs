using BoardGameStore.Infrastructure.Shared.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Infrastructure.Shared
{
    public static class DiRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection collection)
        {
            collection.AddScoped<IMapper, ManualMapper>();
            //collection.AddScoped<IMapper, AutoMapper>();
            //collection.AddScoped<IMapper, MapperlyMapper>();
            return collection;
        }
    }
}
