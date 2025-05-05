using BoardGameStore.Application;
using BoardGameStore.Domain;
using BoardGameStore.Infrastructure.Dapper;
using BoardGameStore.Infrastructure.EFCore;
using BoardGameStore.Infrastructure.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BoardGameStore.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DAPPER
            builder.Services.AddScoped<IDbConnection>(sp =>
                new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDapperRepositories();

            // ENTITY FRAMEWORK CORE
            /*builder.Services.AddDbContext<DbContextEFCore>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddEFCoreRepositories();*/

            // Services unrelated to ORM
            builder.Services.AddInfrastructureServices();
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
