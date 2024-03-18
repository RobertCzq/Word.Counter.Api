namespace Word.Counter.Api.Services;

public interface IWordCounterService
{
    byte[] GetWordCountByteContent(IFormFile file);
}
