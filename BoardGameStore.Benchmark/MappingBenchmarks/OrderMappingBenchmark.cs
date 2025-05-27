using BenchmarkDotNet.Attributes;
using BoardGameStore.Application.DTOs.OrderDTOs;
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
    public class OrderMappingBenchmark
    {
        private static readonly Random _random = new();
        private static readonly Faker _faker = new();

        private IMapper _manualMapper;
        private IMapper _mapperlyMapper;
        private IMapper _autoMapper;

        private AddOrderDTO addOrderDTO;
        private OrderModel orderModel;
        private OrderModel orderModelNoItems;

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
            addOrderDTO = new AddOrderDTO
            {
                TotalPrice = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2),
                UserId = _random.Next(1, 1000),

                Items = Enumerable.Range(0, _random.Next(1, 5)).Select(_ => new AddOrderItemDTO
                {
                    Quantity = _random.Next(1, 101),
                    BoardGameId = _random.Next(1, 1000)
                }).ToList()
            };

            orderModel = new OrderModel
            {
                Id = _random.Next(1, 1000),
                Date = DateTime.Now,
                TotalPrice = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2),
                Status = 0,
                UserId = _random.Next(1, 1000),
                Items = Enumerable.Range(0, _random.Next(1, 5)).Select(_ => new OrderItemModel
                {
                    Id = _random.Next(1, 1000),
                    Quantity = _random.Next(1, 101),
                    BoardGameId = _random.Next(1, 1000),
                    BoardGame = new BoardGameModel 
                    {
                        Id = _random.Next(1, 1000),
                        Name = _faker.Random.Word(),
                        Year = _random.Next(1950, 2025),
                        MinPlayers = _random.Next(1, 4),
                        MaxPlayers = _random.Next(4, 6),
                        Difficulty = 0,
                        AvailableQuantity = _random.Next(1, 1000),
                        Price = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2)
                    }
                }).ToList()
            };

            orderModelNoItems = new OrderModel
            {
                Id = _random.Next(1, 1000),
                Date = DateTime.Now,
                TotalPrice = (decimal)Math.Round(_random.NextDouble() * 100 + 10, 2),
                Status = 0,
                UserId = _random.Next(1, 1000)
            };
        }

        [Benchmark]
        public OrderModel AutoMapper_MapAddOrderDtoToModel()
        {
            return _autoMapper.MapAddOrderDtoToModel(addOrderDTO);
        }

        [Benchmark]
        public OrderModel Mapperly_MapAddOrderDtoToModel()
        {
            return _mapperlyMapper.MapAddOrderDtoToModel(addOrderDTO);
        }

        [Benchmark]
        public OrderModel Manual_MapAddOrderDtoToModel()
        {
            return _manualMapper.MapAddOrderDtoToModel(addOrderDTO);
        }

        [Benchmark]
        public ReturnOrderDTO AutoMapper_MapOrderModelToReturnOrderDTO()
        {
            return _autoMapper.MapOrderModelToReturnOrderDTO(orderModel);
        }

        [Benchmark]
        public ReturnOrderDTO Mapperly_MapOrderModelToReturnOrderDTO()
        {
            return _mapperlyMapper.MapOrderModelToReturnOrderDTO(orderModel);
        }

        [Benchmark]
        public ReturnOrderDTO Manual_MapOrderModelToReturnOrderDTO()
        {
            return _manualMapper.MapOrderModelToReturnOrderDTO(orderModel);
        }

        [Benchmark]
        public ReturnOrderShortDTO AutoMapper_MapOrderModelToReturnOrderShortDTO()
        {
            return _autoMapper.MapOrderModelToReturnOrderShortDTO(orderModelNoItems);
        }

        [Benchmark]
        public ReturnOrderShortDTO Mapperly_MapOrderModelToReturnOrderShortDTO()
        {
            return _mapperlyMapper.MapOrderModelToReturnOrderShortDTO(orderModelNoItems);
        }

        [Benchmark]
        public ReturnOrderShortDTO Manual_MapOrderModelToReturnOrderShortDTO()
        {
            return _manualMapper.MapOrderModelToReturnOrderShortDTO(orderModelNoItems);
        }
    }
}
