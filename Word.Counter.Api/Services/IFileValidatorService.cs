
namespace Word.Counter.Api.Services;

public interface IFileValidatorService
{
    bool IsFileExtensionAllowed(IFormFile file, string[] allowedExtensions);
}
