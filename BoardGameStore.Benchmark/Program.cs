using BenchmarkDotNet.Running;
using BoardGameStore.Benchmark.MappingBenchmarks;
using BoardGameStore.Benchmark.ORMBenchmarks;
using BoardGameStore.Benchmark.ValidationBenchmarks;

namespace BoardGameStore.Benchmark
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Mapping benchmarks
            BenchmarkRunner.Run<BoardGameMappingBenchmark>();
            BenchmarkRunner.Run<UserMappingBenchmark>();
            BenchmarkRunner.Run<OrderMappingBenchmark>();

            // Validation benchmarks
            BenchmarkRunner.Run<BoardGameValidationBenchmark>();
            BenchmarkRunner.Run<UserValidationBenchmark>();
            BenchmarkRunner.Run<OrderValidationBenchmark>();

            // ORM benchmarks
            BenchmarkRunner.Run<BoardGameORMBenchmark>();
            BenchmarkRunner.Run<UserORMBenchmark>();
            BenchmarkRunner.Run<OrderORMBenchmark>();
        }
    }
}
