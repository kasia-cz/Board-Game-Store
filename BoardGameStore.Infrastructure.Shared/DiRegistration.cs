using BoardGameStore.Infrastructure.Shared.Mapping;
using BoardGameStore.Infrastructure.Shared.Mapping.AutoMapper;
using BoardGameStore.Infrastructure.Shared.Mapping.Mapperly;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Infrastructure.Shared
{
    public static class DiRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection collection)
        {
            //collection.AddManualMapping();
            //collection.AddMapperly();
            collection.AddAutoMapper();

            return collection;
        }

        private static IServiceCollection AddManualMapping(this IServiceCollection collection)
        {
            return collection.AddScoped<IMapper, ManualMapper>();
        }

        private static IServiceCollection AddMapperly(this IServiceCollection collection)
        {
            return collection.AddScoped<IMapper, MapperlyMapper>();
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection collection)
        {
            collection.AddScoped<IMapper, AutoMapperMapper>();
            collection.AddAutoMapper(typeof(MappingProfile));

            return collection;
        }
    }
}
