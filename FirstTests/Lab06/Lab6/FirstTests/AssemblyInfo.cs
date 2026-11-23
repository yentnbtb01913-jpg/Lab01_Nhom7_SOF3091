using NUnit.Framework;

// Chỉ các fixture chạy song song; các ca bên trong fixture chạy lần lượt.
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
