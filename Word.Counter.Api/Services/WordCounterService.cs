using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using Word.Counter.Api.Models;

namespace Word.Counter.Api.Services;

public class WordCounterService(ILogger<WordCounterService> logger) : IWordCounterService
{
    private readonly ILogger<WordCounterService> _logger = logger;
    private readonly char[] delimiters = [' ', '.', ',', ';', ':', '?', '!', '"', '(', ')',
        '[', ']', '{', '}', '\n', '\r', '\0'];

    public byte[] GetWordCountByteContent(IFormFile file)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var wordCountDictionary = ParseFileToDictionary(file);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        return CreatetByteContentFromDictionary(wordCountDictionary);
    }

    private Dictionary<string, WordCount> ParseFileToDictionary(IFormFile file)
    {
        try
        {
            var wordCountDictionary = new Dictionary<string, WordCount>(StringComparer.OrdinalIgnoreCase);

            using (var fileStream = new StreamReader(file.OpenReadStream(), Encoding.Default, false, 65536))
            {
                string line;
                while ((line = fileStream.ReadLine()) != null)
                {
                    var words = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        if (!wordCountDictionary.TryGetValue(word, out var currentCount))
                        {
                            wordCountDictionary[word] = currentCount = new WordCount();
                        }

                        currentCount.Count++;
                    }
                }
            }

            return wordCountDictionary.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not read and parse text file");
            return [];
        }
    }

    private byte[] CreatetByteContentFromDictionary(Dictionary<string, WordCount> wordCountDictionary)
    {
        try
        {
            var wordCountString = JsonConvert.SerializeObject(wordCountDictionary);
            return Encoding.Default.GetBytes(wordCountString);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not convert stream to byte");
            return [];
        }
    }
}




