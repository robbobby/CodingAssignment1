// See https://aka.ms/new-console-template for more information

using CodingAssignment2;

Console.WriteLine("Hello, World!");

string text = @"You are going to write a report about what happens to plants and trees through the seasons.

You will base your report on the video below which shows what happens throughout a year. The clip goes through spring, summer, autumn and winter.

First-you-will-plan-what-you-are-going-to-write-by-making-notes-as-you-watch-the-video.

Get a piece of paper and split it into four sections. Write spring, summer, autumn and winter at the top of each box.

Watch the video and write notes in each box about what you can see is happening in each season.";

var analyzer = new TextAnalyser(text);
var result = analyzer.Analyse();

Console.WriteLine($"Sentences: {result.SentenceCount}");
Console.WriteLine($"Words: {result.WordCount}");
Console.WriteLine($"Unique Words: {result.UniqueWordCount}");
Console.WriteLine("Most Common Words:");
foreach (var kv in result.MostCommonWords)
{
    Console.WriteLine($"- {kv.Key}: {kv.Value}");
}