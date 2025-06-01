using BenchmarkDotNet.Attributes;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.EFCore;
using BoardGameStore.Infrastructure.Shared.Mapping;
using BoardGameStore.Infrastructure.Shared.Mapping.Mapperly;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using DapperOrderRepository = BoardGameStore.Infrastructure.Dapper.Repositories.OrderRepository;
using EFCoreOrderRepository = BoardGameStore.Infrastructure.EFCore.Repositories.OrderRepository;

namespace BoardGameStore.Benchmark.ORMBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 100, invocationCount: 1)]
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
        private int id;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IMapper, MapperlyMapper>();
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
                UserId = _random.Next(1, 1001), // IDs 1-1000 must exist in the Users table
                Items = Enumerable.Range(0, _random.Next(1, 5)).Select(_ => new OrderItemModel
                {
                    Quantity = _random.Next(1, 21),
                    BoardGameId = _random.Next(1, 1001), // IDs 1-1000 must exist in the BoardGames table
                }).ToList()
            };

            id = _random.Next(1, 1001);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            _scope.Dispose();
        }

        // for benchmarks below: there must be 1000 board games and 1000 users in the DB before running them

        [Benchmark]
        public async Task Dapper_AddOrder()
        {
            await _dapperOrderRepository.AddOrder(orderModel);
        }

        [Benchmark]
        public async Task EFCore_AddOrder()
        {
            await _efcoreOrderRepository.AddOrder(orderModel);
        }

        // for benchmarks below: there must be 1000 orders and 1000 users in the DB before running them

        [Benchmark]
        public async Task Dapper_GetOrderById()
        {
            await _dapperOrderRepository.GetOrderById(id);
        }

        [Benchmark]
        public async Task EFCore_GetOrderById()
        {
            await _efcoreOrderRepository.GetOrderById(id);
        }

        [Benchmark]
        public async Task Dapper_GetAllOrders()
        {
            await _dapperOrderRepository.GetAllOrders();
        }

        [Benchmark]
        public async Task EFCore_GetAllOrders()
        {
            await _efcoreOrderRepository.GetAllOrders();
        }

        [Benchmark]
        public async Task Dapper_GetUserOrders()
        {
            await _dapperOrderRepository.GetUserOrders(id);
        }

        [Benchmark]
        public async Task EFCore_GetUserOrders()
        {
            await _efcoreOrderRepository.GetUserOrders(id);
        }
    }
}

