namespace Word.Counter.Api.Services;

public class FileValidatorService : IFileValidatorService
{
    public bool IsFileExtensionAllowed(IFormFile file, string[] allowedExtensions)
    {
        var extension = Path.GetExtension(file.FileName);
        return allowedExtensions.Contains(extension);
    }
}

