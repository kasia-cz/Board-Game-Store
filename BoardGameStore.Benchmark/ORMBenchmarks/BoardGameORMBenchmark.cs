using BenchmarkDotNet.Attributes;
using BoardGameStore.Domain.Models;
using BoardGameStore.Domain.RepositoryInterfaces;
using BoardGameStore.Infrastructure.EFCore;
using BoardGameStore.Infrastructure.Shared.Mapping;
using Bogus;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using DapperBoardGameRepository = BoardGameStore.Infrastructure.Dapper.Repositories.BoardGameRepository;
using EFCoreBoardGameRepository = BoardGameStore.Infrastructure.EFCore.Repositories.BoardGameRepository;

namespace BoardGameStore.Benchmark.ORMBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 20, invocationCount: 5)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class BoardGameORMBenchmark
    {
        private const string connectionString = "Server=.\\SQLExpress;Database=BoardGameStoreDB;Trusted_Connection=true;TrustServerCertificate=true;";

        private static readonly Random _random = new();
        private static readonly Faker _faker = new();

        private IServiceScope _scope;
        private IServiceScopeFactory _scopeFactory;

        private IBoardGameRepository _dapperBoardGameRepository;
        private IBoardGameRepository _efcoreBoardGameRepository;

        private BoardGameModel boardGameModel;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IMapper, ManualMapper>();
            services.AddScoped<IBoardGameRepository, DapperBoardGameRepository>();
            services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
            services.AddScoped<IBoardGameRepository, EFCoreBoardGameRepository>();
            services.AddDbContext<DbContextEFCore>(options => options.UseSqlServer(connectionString));

            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _scope = _scopeFactory.CreateScope();
            var repos = _scope.ServiceProvider.GetServices<IBoardGameRepository>().ToList();
            _dapperBoardGameRepository = repos.OfType<DapperBoardGameRepository>().First();
            _efcoreBoardGameRepository = repos.OfType<EFCoreBoardGameRepository>().First();

            boardGameModel = new BoardGameModel
            {
                Name = _faker.Random.Word(),
                Year = _random.Next(1950, 2025),
                MinPlayers = _random.Next(1, 4),
                MaxPlayers = _random.Next(4, 6),
                Difficulty = 0,
                AvailableQuantity = _random.Next(1, 1000),
                Price = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2)
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
            await _dapperBoardGameRepository.AddBoardGame(boardGameModel);
        }

        [Benchmark]
        public async Task EFCore_AddBoardGame()
        {
            await _efcoreBoardGameRepository.AddBoardGame(boardGameModel);
        }
    }
}
