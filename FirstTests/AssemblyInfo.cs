
using NUnit.Framework;

// Cho phép các class test chạy song song.
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Giới hạn tối đa 4 luồng chạy đồng thời.
[assembly: LevelOfParallelism(4)]
