namespace CodingAssignment2.Tests;

public class TextAnalyserTests
{
    [Fact]
    public void Analyse_ShouldNotBreak_WhenSentanceIsEmpty()
    {
        string text = "";
        var Analyser = new TextAnalyser(text);

        var result = Analyser.Analyse();

        Assert.Equal(0, result.SentenceCount);
        Assert.Equal(0, result.WordCount);
        Assert.Equal(0, result.UniqueWordCount);
    }
    
    [Fact]
    public void Analyse_ShouldCountCorrectly()
    {
        string text = "Hello. How are you? Great!";
        var Analyser = new TextAnalyser(text);

        var result = Analyser.Analyse();

        Assert.Equal(3, result.SentenceCount);
    }

    [Fact]
    public void Analyse_ShouldCountWordsIgnoringCaseAndPunctuation()
    {
        string text = "Hello, hello! HELLO?  AiGhT... This is a test.";
        var Analyser = new TextAnalyser(text);

        var result = Analyser.Analyse();

        Assert.Equal(8, result.WordCount);
        Assert.Equal(6, result.UniqueWordCount);
    }

    [Fact]
    public void Analyse_ShouldReturnCorrectTop5()
    {
        string text = "Apple banana apple orange banana apple fruit beer beer beer beer.";
        var Analyser = new TextAnalyser(text);

        var result = Analyser.Analyse();

        var expected = new List<KeyValuePair<string, int>> {
            new("beer", 4),
            new("apple", 3),
            new("banana", 2),
            new("fruit", 1),
            new("orange", 1)
        };

        Assert.Equal(expected, result.MostCommonWords);
    }

    [Fact]
    public void Analyse_ShouldReturnZeroes()
    {
        var Analyser = new TextAnalyser("");

        var result = Analyser.Analyse();

        Assert.Equal(0, result.SentenceCount);
        Assert.Equal(0, result.WordCount);
        Assert.Equal(0, result.UniqueWordCount);
        Assert.Empty(result.MostCommonWords);
    }

    [Fact]
    public void Analyse_ShouldBeSanitized()
    {
        string text = "It's going to fail this test because I haven't accounts for the character \"'\" it's ok though because it's a test.";
        var Analyser = new TextAnalyser(text);

        var result = Analyser.Analyse();

        Assert.Equal(20, result.WordCount);
        Assert.Contains(result.MostCommonWords, kv => kv.Key == "it's" && kv.Value == 3);
    }
}
