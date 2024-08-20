using BenchmarkDotNet.Running;
using FuzzySharp;

BenchmarkRunner.Run(typeof(Program).Assembly);

//Console.WriteLine(Fuzz.WeightedRatio("The quick brown fox jimps ofver the small lazy dog", "the quick brown fox jumps over the small lazy dog"));