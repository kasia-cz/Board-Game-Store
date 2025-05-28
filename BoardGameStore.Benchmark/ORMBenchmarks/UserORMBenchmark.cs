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
using DapperUserRepository = BoardGameStore.Infrastructure.Dapper.Repositories.UserRepository;
using EFCoreUserRepository = BoardGameStore.Infrastructure.EFCore.Repositories.UserRepository;

namespace BoardGameStore.Benchmark.ORMBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 20, invocationCount: 5)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class UserORMBenchmark
    {
        private const string connectionString = "Server=.\\SQLExpress;Database=BoardGameStoreDB;Trusted_Connection=true;TrustServerCertificate=true;";

        private static readonly Faker _faker = new();

        private IServiceScope _scope;
        private IServiceScopeFactory _scopeFactory;

        private IUserRepository _dapperUserRepository;
        private IUserRepository _efcoreUserRepository;

        private UserModel userModel;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IMapper, ManualMapper>();
            services.AddScoped<IUserRepository, DapperUserRepository>();
            services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
            services.AddScoped<IUserRepository, EFCoreUserRepository>();
            services.AddDbContext<DbContextEFCore>(options => options.UseSqlServer(connectionString));

            var provider = services.BuildServiceProvider();
            _scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _scope = _scopeFactory.CreateScope();
            var repos = _scope.ServiceProvider.GetServices<IUserRepository>().ToList();
            _dapperUserRepository = repos.OfType<DapperUserRepository>().First();
            _efcoreUserRepository = repos.OfType<EFCoreUserRepository>().First();

            userModel = new UserModel
            {
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                PhoneNumber = _faker.Phone.PhoneNumber("###-###-####"),
                DateOfBirth = _faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1)),
                Address = new AddressModel
                {
                    City = _faker.Address.City(),
                    AddressLine = _faker.Address.StreetAddress(),
                    PostalCode = _faker.Address.ZipCode("##-###")
                }
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
            await _dapperUserRepository.AddUser(userModel);
        }

        [Benchmark]
        public async Task EFCore_AddUser()
        {
            await _efcoreUserRepository.AddUser(userModel);
        }
    }
}
