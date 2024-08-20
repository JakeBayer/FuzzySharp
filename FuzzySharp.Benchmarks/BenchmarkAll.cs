using BenchmarkDotNet.Attributes;
using FuzzySharp.Extractor;
using FuzzySharp.PreProcess;

namespace FuzzySharp.Benchmarks;

[MemoryDiagnoser]
public class BenchmarkAll
{
    [Benchmark]
    public int Ratio1()
    {
        return Fuzz.Ratio("mysmilarstring", "myawfullysimilarstirng");
    }

    [Benchmark]
    public int Ratio2()
    {
        return Fuzz.Ratio("mysmilarstring", "mysimilarstring");
    }

    [Benchmark]
    public int PartialRatio()
    {
        return Fuzz.PartialRatio("similar", "somewhresimlrbetweenthisstring");
    }

    [Benchmark]
    public int TokenSortRatio()
    {
        return Fuzz.TokenSortRatio("order words out of", "  words out of order");
    }

    [Benchmark]
    public int PartialTokenSortRatio()
    {
        return Fuzz.PartialTokenSortRatio("order words out of", "  words out of order");
    }

    [Benchmark]
    public int TokenSetRatio()
    {
        return Fuzz.TokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear");
    }

    [Benchmark]
    public int PartialTokenSetRatio()
    {
        return Fuzz.PartialTokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear");
    }

    [Benchmark]
    public int WeightedRatio()
    {
        return Fuzz.WeightedRatio("The quick brown fox jimps ofver the small lazy dog", "the quick brown fox jumps over the small lazy dog");
    }

    [Benchmark]
    public int TokenInitialismRatio1()
    {
        return Fuzz.TokenInitialismRatio("NASA", "National Aeronautics and Space Administration");
    }

    [Benchmark]
    public int TokenInitialismRatio2()
    {
        return Fuzz.TokenInitialismRatio("NASA", "National Aeronautics Space Administration");
    }

    [Benchmark]
    public int TokenInitialismRatio3()
    {
        return Fuzz.TokenInitialismRatio("NASA", "National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899");
    }

    [Benchmark]
    public int PartialTokenInitialismRatio()
    {
        return Fuzz.PartialTokenInitialismRatio("NASA", "National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899");
    }

    [Benchmark]
    public int TokenAbbreviationRatio()
    {
        return Fuzz.TokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full);
    }

    [Benchmark]
    public int PartialTokenAbbreviationRatio()
    {
        return Fuzz.PartialTokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full);
    }

    private static readonly string[][] Events =
    [
        ["chicago cubs vs new york mets", "CitiField", "2011-05-11", "8pm"],
        ["new york yankees vs boston red sox", "Fenway Park", "2011-05-11", "8pm"],
        ["atlanta braves vs pittsburgh pirates", "PNC Park", "2011-05-11", "8pm"]
    ];

    private static readonly string[] Query = ["new york mets vs chicago cubs", "CitiField", "2017-03-19", "8pm"];

    [Benchmark]
    public ExtractedResult<string[]> ExtractOne()
    {
        return Process.ExtractOne(Query, Events, static strings => strings[0]);
    }

    [Benchmark]
    public int LevenshteinDistance()
    {
        return Levenshtein.EditDistance("chicago cubs vs new york mets".AsSpan(), "new york mets vs chicago cubs".AsSpan());
    }

    [Benchmark]
    public int FastenshteinDistance()
    {
        return Fastenshtein.Levenshtein.Distance("chicago cubs vs new york mets", "new york mets vs chicago cubs");
    }
}