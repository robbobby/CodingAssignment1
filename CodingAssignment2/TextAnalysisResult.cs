namespace CodingAssignment2;

public class TextAnalysisResult
{
    public int SentenceCount { get; set; }
    public int WordCount { get; set; }
    public int UniqueWordCount { get; set; }
    public List<KeyValuePair<string, int>> MostCommonWords { get; set; }
}
