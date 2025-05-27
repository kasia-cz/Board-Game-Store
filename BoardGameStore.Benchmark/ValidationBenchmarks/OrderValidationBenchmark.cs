using BenchmarkDotNet.Attributes;
using BoardGameStore.Application.DTOs.OrderDTOs;
using BoardGameStore.Application.Validation.DataAnnotations;
using BoardGameStore.Application.Validation.FluentValidation.Validators;
using BoardGameStore.Application.Validation.FluentValidation;
using BoardGameStore.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Benchmark.ValidationBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 20, invocationCount: 5)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class OrderValidationBenchmark
    {
        private static readonly Random _random = new();

        private IValidationService<AddOrderDTO> _dataAnnotationsValidator;
        private IValidationService<AddOrderDTO> _fluentValidator;

        private AddOrderDTO addOrderDTO;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IValidationService<AddOrderDTO>, DataAnnotationsValidationService<AddOrderDTO>>();
            services.AddScoped<IValidationService<AddOrderDTO>, FluentValidationService<AddOrderDTO>>();
            services.AddValidatorsFromAssemblyContaining(typeof(OrderValidator));

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var validators = scope.ServiceProvider.GetServices<IValidationService<AddOrderDTO>>().ToList();
            _dataAnnotationsValidator = validators.OfType<DataAnnotationsValidationService<AddOrderDTO>>().First();
            _fluentValidator = validators.OfType<FluentValidationService<AddOrderDTO>>().First();
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
        }

        [Benchmark]
        public void DataAnnotations_ValidateOrder()
        {
            _dataAnnotationsValidator.ValidateAndThrow(addOrderDTO);
        }

        [Benchmark]
        public void FluentValidation_ValidateOrder()
        {
            _fluentValidator.ValidateAndThrow(addOrderDTO);
        }
    }
}
