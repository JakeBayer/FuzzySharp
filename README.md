# FuzzySharp
C# .NET fuzzy string matching implementation of Seat Geek's well known python FuzzyWuzzy algorithm. 

# Release Notes:
+v.2.0.0
+--As of 2.0.0, all empty strings will return a score of 0. Prior, the partial scoring system would return a score of 100, regardless if the other input had correct value or not. This was a result of the partial scoring system returning an empty set for the matching blocks -- as a result, this led to incorrrect values in the composite scores; several of them (token set, token sort), relied on the prior value of empty strings.

As a result, many 1.X.X unit test may be broken with the 2.X.X upgrade, but it is within the expertise fo all the 1.X.X developers to recommednd the upgrade to the 2.X.X series regardless, should their version accommodate it or not, as it is closer to the ideal behavior of the library.


## Usage

Install-Package FuzzySharp

#### Simple Ratio
```csharp
Fuzz.Ratio("mysmilarstring","myawfullysimilarstirng")
72
Fuzz.Ratio("mysmilarstring","mysimilarstring")
97
```

#### Partial Ratio
```csharp
Fuzz.PartialRatio("similar", "somewhresimlrbetweenthisstring")
71
```

#### Token Sort Ratio
```csharp
Fuzz.TokenSortRatio("order words out of","  words out of order")
100
Fuzz.PartialTokenSortRatio("order words out of","  words out of order")
100
```

#### Token Set Ratio
```csharp
Fuzz.TokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear")
100
Fuzz.PartialTokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear")
100
```

#### Token Initialism Ratio
```csharp
Fuzz.TokenInitialismRatio("NASA", "National Aeronautics and Space Administration");
89
Fuzz.TokenInitialismRatio("NASA", "National Aeronautics Space Administration");
100

Fuzz.TokenInitialismRatio("NASA", "National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899");
53
Fuzz.PartialTokenInitialismRatio("NASA", "National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899");
100
```

#### Token Abbreviation Ratio
```csharp
Fuzz.TokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full);
40
Fuzz.PartialTokenAbbreviationRatio("bl 420", "Baseline section 420", PreprocessMode.Full);
50      
```


#### Weighted Ratio
```csharp
Fuzz.WeightedRatio("The quick brown fox jimps ofver the small lazy dog", "the quick brown fox jumps over the small lazy dog")
95
```

#### Process
```csharp
Process.ExtractOne("cowboys", new[] { "Atlanta Falcons", "New York Jets", "New York Giants", "Dallas Cowboys"})
(string: Dallas Cowboys, score: 90, index: 3)
```
```csharp
Process.ExtractTop("goolge", new[] { "google", "bing", "facebook", "linkedin", "twitter", "googleplus", "bingnews", "plexoogl" }, limit: 3);
[(string: google, score: 83, index: 0), (string: googleplus, score: 75, index: 5), (string: plexoogl, score: 43, index: 7)]
```
```csharp
Process.ExtractAll("goolge", new [] {"google", "bing", "facebook", "linkedin", "twitter", "googleplus", "bingnews", "plexoogl" })
[(string: google, score: 83, index: 0), (string: bing, score: 22, index: 1), (string: facebook, score: 29, index: 2), (string: linkedin, score: 29, index: 3), (string: twitter, score: 15, index: 4), (string: googleplus, score: 75, index: 5), (string: bingnews, score: 29, index: 6), (string: plexoogl, score: 43, index: 7)]
// score cutoff
Process.ExtractAll("goolge", new[] { "google", "bing", "facebook", "linkedin", "twitter", "googleplus", "bingnews", "plexoogl" }, cutoff: 40)
[(string: google, score: 83, index: 0), (string: googleplus, score: 75, index: 5), (string: plexoogl, score: 43, index: 7)]
```
```csharp
Process.ExtractSorted("goolge", new [] {"google", "bing", "facebook", "linkedin", "twitter", "googleplus", "bingnews", "plexoogl" })
[(string: google, score: 83, index: 0), (string: googleplus, score: 75, index: 5), (string: plexoogl, score: 43, index: 7), (string: facebook, score: 29, index: 2), (string: linkedin, score: 29, index: 3), (string: bingnews, score: 29, index: 6), (string: bing, score: 22, index: 1), (string: twitter, score: 15, index: 4)]
```

Extraction will use `WeightedRatio` and `full process` by default. Override these in the method parameters to use different scorers and processing.
Here we use the Fuzz.Ratio scorer and keep the strings as is, instead of Full Process (which will .ToLowercase() before comparing)
```csharp
Process.ExtractOne("cowboys", new[] { "Atlanta Falcons", "New York Jets", "New York Giants", "Dallas Cowboys" }, s => s, ScorerCache.Get<DefaultRatioScorer>());
(string: Dallas Cowboys, score: 57, index: 3)
```

Extraction can operate on objects of similar type. Use the "process" parameter to reduce the object to the string which it should be compared on. In the following example, the object is an array that contains the matchup, the arena, the date, and the time. We are matching on the first (0 index) parameter, the matchup.
```csharp
var events = new[]
{
    new[] { "chicago cubs vs new york mets", "CitiField", "2011-05-11", "8pm" },
    new[] { "new york yankees vs boston red sox", "Fenway Park", "2011-05-11", "8pm" },
    new[] { "atlanta braves vs pittsburgh pirates", "PNC Park", "2011-05-11", "8pm" },
};
var query = new[] { "new york mets vs chicago cubs", "CitiField", "2017-03-19", "8pm" };
var best = Process.ExtractOne(query, events, strings => strings[0]);

best: (value: { "chicago cubs vs new york mets", "CitiField", "2011-05-11", "8pm" }, score: 95, index: 0)
```

### FuzzySharp in Different Languages
FuzzySharp was written with English in mind, and as such the Default string preprocessor only looks at English alphanumeric characters in the input strings, and will strip all others out. However, the `Extract` methods in the `Process` class do provide the option to specify your own string preprocessor. If this parameter is omitted, the Default will be used. However if you provide your own, the provided one will be used, so you are free to provide your own criteria for whatever character set you want to admit. For instance, using the parameter `(s) => s` will prevent the string from being altered at all before being run through the similarity algorithms.

E.g.,

```csharp
var query = "strng";
var choices = new [] { "stríng", "stráng", "stréng" };
var results = Process.ExtractAll(query, choices, (s) => s);
```
The above will run the similarity algorithm on all the choices without stripping out the accented characters.

### Using Different Scorers
Scoring strategies are stateless, and as such should be static. However, in order to get them to share all the code they have in common via inheritance, making them static was not possible.
Currently one way around having to new up an instance everytime you want to use one is to use the cache. This will ensure only one instance of each scorer ever exists.
```csharp
var ratio = ScorerCache.Get<DefaultRatioScorer>();
var partialRatio = ScorerCache.Get<PartialRatioScorer>();
var tokenSet = ScorerCache.Get<TokenSetScorer>();
var partialTokenSet = ScorerCache.Get<PartialTokenSetScorer>();
var tokenSort = ScorerCache.Get<TokenSortScorer>();
var partialTokenSort = ScorerCache.Get<PartialTokenSortScorer>();
var tokenAbbreviation = ScorerCache.Get<TokenAbbreviationScorer>();
var partialTokenAbbreviation = ScorerCache.Get<PartialTokenAbbreviationScorer>();
var weighted = ScorerCache.Get<WeightedRatioScorer>();
```

## Credits

- SeatGeek
- Adam Cohen
- David Necas (python-Levenshtein)
- Mikko Ohtamaa (python-Levenshtein)
- Antti Haapala (python-Levenshtein)
- Panayiotis (Java implementation I heavily borrowed from)
