using BenchmarkDotNet.Attributes;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Application.Mapping;
using BoardGameStore.Application.Mapping.AutoMapper;
using BoardGameStore.Application.Mapping.Mapperly;
using BoardGameStore.Domain.Models;
using Bogus;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Benchmark.MappingBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 100, invocationCount: 1)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class UserMappingBenchmark
    {
        private static readonly Random _random = new();
        private static readonly Faker _faker = new();

        private IMapper _manualMapper;
        private IMapper _mapperlyMapper;
        private IMapper _autoMapper;

        private AddUserDTO addUserDTO;
        private UserModel userModel;
        private UserModel userModelNoAddress;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IMapper, ManualMapper>();
            services.AddScoped<IMapper, MapperlyMapper>();
            services.AddScoped<IMapper, AutoMapperMapper>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var mappers = scope.ServiceProvider.GetServices<IMapper>().ToList();
            _manualMapper = mappers.OfType<ManualMapper>().First();
            _mapperlyMapper = mappers.OfType<MapperlyMapper>().First();
            _autoMapper = mappers.OfType<AutoMapperMapper>().First();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            addUserDTO = new AddUserDTO
            {
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                PhoneNumber = _faker.Phone.PhoneNumber("###-###-####"),
                DateOfBirth = _faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1)),
                Address = new AddAddressDTO
                {
                    City = _faker.Address.City(),
                    AddressLine = _faker.Address.StreetAddress(),
                    PostalCode = _faker.Address.ZipCode("##-###")
                }
            };

            userModel = new UserModel
            {
                Id = _random.Next(1, 1000),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                PhoneNumber = _faker.Phone.PhoneNumber("###-###-####"),
                DateOfBirth = _faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1)),
                Address = new AddressModel
                {
                    Id = _random.Next(1, 1000),
                    City = _faker.Address.City(),
                    AddressLine = _faker.Address.StreetAddress(),
                    PostalCode = _faker.Address.ZipCode("##-###")
                }
            };

            userModelNoAddress = new UserModel
            {
                Id = _random.Next(1, 1000),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                Email = _faker.Internet.Email(),
                PhoneNumber = _faker.Phone.PhoneNumber("###-###-####"),
                DateOfBirth = _faker.Date.Between(new DateTime(1950, 1, 1), new DateTime(2000, 1, 1))
            };
        }

        [Benchmark]
        public UserModel AutoMapper_MapAddUserDtoToModel()
        {
            return _autoMapper.MapAddUserDtoToModel(addUserDTO);
        }

        [Benchmark]
        public UserModel Mapperly_MapAddUserDtoToModel()
        {
            return _mapperlyMapper.MapAddUserDtoToModel(addUserDTO);
        }

        [Benchmark]
        public UserModel Manual_MapAddUserDtoToModel()
        {
            return _manualMapper.MapAddUserDtoToModel(addUserDTO);
        }

        [Benchmark]
        public ReturnUserDTO AutoMapper_MapUserModelToReturnUserDTO()
        {
            return _autoMapper.MapUserModelToReturnUserDTO(userModel);
        }

        [Benchmark]
        public ReturnUserDTO Mapperly_MapUserModelToReturnUserDTO()
        {
            return _mapperlyMapper.MapUserModelToReturnUserDTO(userModel);
        }

        [Benchmark]
        public ReturnUserDTO Manual_MapUserModelToReturnUserDTO()
        {
            return _manualMapper.MapUserModelToReturnUserDTO(userModel);
        }

        [Benchmark]
        public ReturnUserShortDTO AutoMapper_MapUserModelToReturnUserShortDTO()
        {
            return _autoMapper.MapUserModelToReturnUserShortDTO(userModelNoAddress);
        }

        [Benchmark]
        public ReturnUserShortDTO Mapperly_MapUserModelToReturnUserShortDTO()
        {
            return _mapperlyMapper.MapUserModelToReturnUserShortDTO(userModelNoAddress);
        }

        [Benchmark]
        public ReturnUserShortDTO Manual_MapUserModelToReturnUserShortDTO()
        {
            return _manualMapper.MapUserModelToReturnUserShortDTO(userModelNoAddress);
        }
    }
}
