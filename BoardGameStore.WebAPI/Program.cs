using BoardGameStore.Application;
using BoardGameStore.Domain;
using BoardGameStore.Infrastructure.Dapper;
using BoardGameStore.Infrastructure.EFCore;

namespace BoardGameStore.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DAPPER
            // todo: add dbcontext
            // builder.Services.AddDapperRepositories();

            // ENTITY FRAMEWORK CORE
            // todo: add dbcontext
            builder.Services.AddEFCoreRepositories();

            // Services unrelated to ORM
            builder.Services.AddDomainServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
