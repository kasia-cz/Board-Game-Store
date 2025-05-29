using BenchmarkDotNet.Attributes;
using BoardGameStore.Application.DTOs.UserDTOs;
using BoardGameStore.Application.Validation.DataAnnotations;
using BoardGameStore.Application.Validation.FluentValidation.Validators;
using BoardGameStore.Application.Validation.FluentValidation;
using BoardGameStore.Application.Validation;
using Bogus;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameStore.Benchmark.ValidationBenchmarks
{
    [SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 100, invocationCount: 1)]
    [MemoryDiagnoser]
    [CsvExporter]
    [MinColumn, MaxColumn]
    public class UserValidationBenchmark
    {
        private static readonly Faker _faker = new();

        private IValidationService<AddUserDTO> _dataAnnotationsValidator;
        private IValidationService<AddUserDTO> _fluentValidator;

        private AddUserDTO addUserDTO;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var services = new ServiceCollection();
            services.AddScoped<IValidationService<AddUserDTO>, DataAnnotationsValidationService<AddUserDTO>>();
            services.AddScoped<IValidationService<AddUserDTO>, FluentValidationService<AddUserDTO>>();
            services.AddValidatorsFromAssemblyContaining(typeof(UserValidator));

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var validators = scope.ServiceProvider.GetServices<IValidationService<AddUserDTO>>().ToList();
            _dataAnnotationsValidator = validators.OfType<DataAnnotationsValidationService<AddUserDTO>>().First();
            _fluentValidator = validators.OfType<FluentValidationService<AddUserDTO>>().First();
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
        }

        [Benchmark]
        public void DataAnnotations_ValidateUser()
        {
            _dataAnnotationsValidator.ValidateAndThrow(addUserDTO);
        }

        [Benchmark]
        public void FluentValidation_ValidateUser()
        {
            _fluentValidator.ValidateAndThrow(addUserDTO);
        }
    }
}
