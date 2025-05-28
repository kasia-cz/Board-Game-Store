using BenchmarkDotNet.Attributes;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.EFCore;
using BoardGameStore.Infrastructure.Shared.Mapping;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using DapperOrderRepository = BoardGameStore.Infrastructure.Dapper.Repositories.OrderRepository;
using EFCoreOrderRepository = BoardGameStore.Infrastructure.EFCore.Repositories.OrderRepository;

namespace BoardGameStore.Benchmark.ORMBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 20, invocationCount: 5)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class OrderORMBenchmark
    {
        private const string connectionString = "Server=.\\SQLExpress;Database=BoardGameStoreDB;Trusted_Connection=true;TrustServerCertificate=true;";

        private static readonly Random _random = new();

        private IServiceScope _scope;
        private IServiceScopeFactory _scopeFactory;

        private IOrderRepository _dapperOrderRepository;
        private IOrderRepository _efcoreOrderRepository;

        private OrderModel orderModel;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IMapper, ManualMapper>();
            services.AddScoped<IOrderRepository, DapperOrderRepository>();
            services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
            services.AddScoped<IOrderRepository, EFCoreOrderRepository>();
            services.AddDbContext<DbContextEFCore>(options => options.UseSqlServer(connectionString));

            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _scope = _scopeFactory.CreateScope();
            var repos = _scope.ServiceProvider.GetServices<IOrderRepository>().ToList();
            _dapperOrderRepository = repos.OfType<DapperOrderRepository>().First();
            _efcoreOrderRepository = repos.OfType<EFCoreOrderRepository>().First();

            orderModel = new OrderModel
            {
                Date = DateTime.Now,
                TotalPrice = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2),
                Status = 0,
                UserId = _random.Next(1, 1000), // IDs 1-1000 must exist in the Users table
                Items = Enumerable.Range(0, _random.Next(1, 5)).Select(_ => new OrderItemModel
                {
                    Quantity = _random.Next(1, 101),
                    BoardGameId = _random.Next(1, 1000), // IDs 1-1000 must exist in the BoardGames table
                }).ToList()
            };
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            _scope.Dispose();
        }

        [Benchmark]
        public async Task Dapper_AddBoardGame()
        {
            await _dapperOrderRepository.AddOrder(orderModel);
        }

        [Benchmark]
        public async Task EFCore_AddOrder()
        {
            await _efcoreOrderRepository.AddOrder(orderModel);
        }
    }
}

