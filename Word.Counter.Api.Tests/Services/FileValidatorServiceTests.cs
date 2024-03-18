using Microsoft.AspNetCore.Http;
using Moq;
using Word.Counter.Api.Services;
using Word.Counter.Api.Tests.Fixtures;

namespace Word.Counter.Api.Tests.Services;

public class FileValidatorServiceTests
{
    [Fact]
    public void IsFileExtensionAllowed_Returns_False_WhenExtensionIsNotAllowed()
    {
        //Arrange
        var fileValidatorService = new FileValidatorService();
        var fileMock = new Mock<IFormFile>().Object;
        var allowedExtensions = new[] { ".txt" };

        //Act
        var result = fileValidatorService.IsFileExtensionAllowed(fileMock, allowedExtensions);

        //Assert
        result.Equals(false);
    }

    [Fact]
    public void IsFileExtensionAllowed_Returns_True_WhenExtensionIsAllowed()
    {
        //Arrange
        var fileValidatorService = new FileValidatorService();
        var fileMock = FileFixture.SetupFile("big.txt");
        var allowedExtensions = new[] { ".txt" };

        //Act
        var result = fileValidatorService.IsFileExtensionAllowed(fileMock, allowedExtensions);

        //Assert
        result.Equals(true);
    }
}
