using BenchmarkDotNet.Running;
using BoardGameStore.Benchmark.MappingBenchmarks;

namespace BoardGameStore.Benchmark
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<MappingBoardGameBenchmark>();
            BenchmarkRunner.Run<MappingUserBenchmark>();
            BenchmarkRunner.Run<MappingOrderBenchmark>();
        }
    }
}
