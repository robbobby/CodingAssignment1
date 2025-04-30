using System.Text.RegularExpressions;

namespace CodingAssignment2;

public class TextAnalyser
{
    private readonly string _text;
    private readonly List<string> _sentences;
    private readonly List<string> _words;
    private readonly Dictionary<string, int> _wordCount;
    
    public TextAnalyser(string text)
    {
        _text = text;
        _sentences = SplitSentences(_text);
        _words = ExtractWords(_text);
        _wordCount = CountWords(_words);
    }
    
    public TextAnalysisResult Analyse()
    {
        return new TextAnalysisResult
        {
            SentenceCount = _sentences.Count,
            WordCount = _words.Count,
            UniqueWordCount = _wordCount.Count,
            MostCommonWords = _wordCount
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .Take(5)
                .ToList()
        };
    }

    private List<string> SplitSentences(string text)
    {
        var split = Regex.Split(text, @"[.!?]+")
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();
        return split;
    }

    private List<string> ExtractWords(string text)
    {
        return Regex.Matches(text.ToLower(), @"\b[a-zA-Z]+(?:'[a-zA-Z]+)?\b")
            .Select(m => m.Value)
            .ToList();
    }

    private Dictionary<string, int> CountWords(List<string> words)
    {
        var frequencies = new Dictionary<string, int>();
        foreach(var word in words.Where(word => !frequencies.TryAdd(word, 1)))
        {
            frequencies[word]++;
        }

        return frequencies;
    }
}
