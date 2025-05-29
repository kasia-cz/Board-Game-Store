using BenchmarkDotNet.Attributes;
using BoardGameStore.Application.DTOs.BoardGameDTOs;
using BoardGameStore.Application.Validation;
using BoardGameStore.Application.Validation.DataAnnotations;
using BoardGameStore.Application.Validation.FluentValidation;
using BoardGameStore.Application.Validation.FluentValidation.Validators;
using Bogus;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Benchmark.ValidationBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 100, invocationCount: 1)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class BoardGameValidationBenchmark
    {
        private static readonly Random _random = new();
        private static readonly Faker _faker = new();

        private IValidationService<AddBoardGameDTO> _dataAnnotationsValidator;
        private IValidationService<AddBoardGameDTO> _fluentValidator;

        private AddBoardGameDTO addBoardGameDTO;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IValidationService<AddBoardGameDTO>, DataAnnotationsValidationService<AddBoardGameDTO>>();
            services.AddScoped<IValidationService<AddBoardGameDTO>, FluentValidationService<AddBoardGameDTO>>();
            services.AddValidatorsFromAssemblyContaining(typeof(BoardGameValidator));

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var validators = scope.ServiceProvider.GetServices<IValidationService<AddBoardGameDTO>>().ToList();
            _dataAnnotationsValidator = validators.OfType<DataAnnotationsValidationService<AddBoardGameDTO>>().First();
            _fluentValidator = validators.OfType<FluentValidationService<AddBoardGameDTO>>().First();
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
        }

        [Benchmark]
        public void DataAnnotations_ValidateBoardGame()
        {
            _dataAnnotationsValidator.ValidateAndThrow(addBoardGameDTO);
        }

        [Benchmark]
        public void FluentValidation_ValidateBoardGame()
        {
            _fluentValidator.ValidateAndThrow(addBoardGameDTO);
        }
    }
}
