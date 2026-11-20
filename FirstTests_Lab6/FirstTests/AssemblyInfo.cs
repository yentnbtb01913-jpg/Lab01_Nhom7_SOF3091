using NUnit.Framework;

// Chạy song song các fixture độc lập. Các test trong cùng fixture vẫn tuần tự.
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
