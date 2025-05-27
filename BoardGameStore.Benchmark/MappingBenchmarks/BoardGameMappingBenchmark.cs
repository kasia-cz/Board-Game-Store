using BenchmarkDotNet.Attributes;
using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.Mapping;
using BoardGameStore.Application.Mapping.AutoMapper;
using BoardGameStore.Application.Mapping.Mapperly;
using BoardGameStore.Domain.Models;
using Bogus;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Benchmark.MappingBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 20, invocationCount: 5)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class BoardGameMappingBenchmark
    {
        private static readonly Random _random = new();
        private static readonly Faker _faker = new();

        private IMapper _manualMapper;
        private IMapper _mapperlyMapper;
        private IMapper _autoMapper;

        private AddBoardGameDTO addBoardGameDTO;
        private BoardGameModel boardGameModel;

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
            addBoardGameDTO = new AddBoardGameDTO
            {
                Name = _faker.Random.Word(),
                Year = _random.Next(1950, 2025),
                MinPlayers = _random.Next(1, 4),
                MaxPlayers = _random.Next(4, 6),
                Difficulty = 0,
                AvailableQuantity = _random.Next(1, 1000),
                Price = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2)
            };

            boardGameModel = new BoardGameModel
            {
                Id = _random.Next(1, 1000),
                Name = _faker.Random.Word(),
                Year = _random.Next(1950, 2025),
                MinPlayers = _random.Next(1, 4),
                MaxPlayers = _random.Next(4, 6),
                Difficulty = 0,
                AvailableQuantity = _random.Next(1, 1000),
                Price = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2)
            };
        }

        [Benchmark]
        public BoardGameModel AutoMapper_MapAddBoardGameDtoToModel()
        {
            return _autoMapper.MapAddBoardGameDtoToModel(addBoardGameDTO);
        }

        [Benchmark]
        public BoardGameModel Mapperly_MapAddBoardGameDtoToModel()
        {
            return _mapperlyMapper.MapAddBoardGameDtoToModel(addBoardGameDTO);
        }

        [Benchmark]
        public BoardGameModel Manual_MapAddBoardGameDtoToModel()
        {
            return _manualMapper.MapAddBoardGameDtoToModel(addBoardGameDTO);
        }

        [Benchmark]
        public ReturnBoardGameDTO AutoMapper_MapBoardGameModelToReturnBoardGameDTO()
        {
            return _autoMapper.MapBoardGameModelToReturnBoardGameDTO(boardGameModel);
        }

        [Benchmark]
        public ReturnBoardGameDTO Mapperly_MapBoardGameModelToReturnBoardGameDTO()
        {
            return _mapperlyMapper.MapBoardGameModelToReturnBoardGameDTO(boardGameModel);
        }

        [Benchmark]
        public ReturnBoardGameDTO Manual_MapBoardGameModelToReturnBoardGameDTO()
        {
            return _manualMapper.MapBoardGameModelToReturnBoardGameDTO(boardGameModel);
        }

        [Benchmark]
        public ReturnBoardGameShortDTO AutoMapper_MapBoardGameModelToReturnBoardGameShortDTO()
        {
            return _autoMapper.MapBoardGameModelToReturnBoardGameShortDTO(boardGameModel);
        }

        [Benchmark]
        public ReturnBoardGameShortDTO Mapperly_MapBoardGameModelToReturnBoardGameShortDTO()
        {
            return _mapperlyMapper.MapBoardGameModelToReturnBoardGameShortDTO(boardGameModel);
        }

        [Benchmark]
        public ReturnBoardGameShortDTO Manual_MapBoardGameModelToReturnBoardGameShortDTO()
        {
            return _manualMapper.MapBoardGameModelToReturnBoardGameShortDTO(boardGameModel);
        }
    }
}
