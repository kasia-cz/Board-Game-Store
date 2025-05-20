using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Application.Interfaces;
using BoardGameStore.Application.Mapping;
using BoardGameStore.Application.Mapping.AutoMapper;
using BoardGameStore.Application.Mapping.Mapperly;
using BoardGameStore.Application.Services;
using BoardGameStore.Application.Validation;
using BoardGameStore.Application.Validation.DataAnnotations;
using BoardGameStore.Application.Validation.FluentValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BoardGameStore.Application
{
    public static class DiRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddScoped<IBoardGameAppService, BoardGameAppService>();
            collection.AddScoped<IUserAppService, UserAppService>();
            collection.AddScoped<IOrderAppService, OrderAppService>();

            // MAPPING
            //collection.AddManualMapping();
            //collection.AddMapperly();
            collection.AddAutoMapper();

            // VALIDATION
            //collection.AddDataAnnotations();
            collection.AddFluentValidation();

            return collection;
        }

        private static IServiceCollection AddManualMapping(this IServiceCollection collection)
        {
            return collection.AddScoped<IMapper, ManualMapper>();
        }

        private static IServiceCollection AddMapperly(this IServiceCollection collection)
        {
            return collection.AddScoped<IMapper, MapperlyMapper>(); ;
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection collection)
        {
            collection.AddScoped<IMapper, AutoMapperMapper>();
            collection.AddAutoMapper(Assembly.GetExecutingAssembly());

            return collection;
        }

        private static IServiceCollection AddDataAnnotations(this IServiceCollection collection)
        {
            collection.AddScoped<IValidationService<AddBoardGameDTO>, DataAnnotationsValidationService<AddBoardGameDTO>>();
            collection.AddScoped<IValidationService<AddUserDTO>, DataAnnotationsValidationService<AddUserDTO>>();
            collection.AddScoped<IValidationService<AddOrderDTO>, DataAnnotationsValidationService<AddOrderDTO>>();

            return collection;
        }

        private static IServiceCollection AddFluentValidation(this IServiceCollection collection)
        {
            collection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            collection.AddScoped<IValidationService<AddBoardGameDTO>, FluentValidationService<AddBoardGameDTO>>();
            collection.AddScoped<IValidationService<AddUserDTO>, FluentValidationService<AddUserDTO>>();
            collection.AddScoped<IValidationService<AddOrderDTO>, FluentValidationService<AddOrderDTO>>();

            return collection;
        }
    }
}
