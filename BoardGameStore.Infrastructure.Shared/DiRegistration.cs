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
            //collection.AddScoped<IMapper, ManualMapper>();
            //collection.AddScoped<IMapper, AutoMapperMapper>();
            //collection.AddAutoMapper(typeof(MappingProfile));
            collection.AddScoped<IMapper, MapperlyMapper>();
            return collection;
        }
    }
}
