using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Word.Counter.Api.Services;
using Word.Counter.Api.Tests.Fixtures;

namespace Word.Counter.Api.Tests.Services;

public class WordCounterServiceTests
{
    [Fact]
    public void GetWordCountByteContent_Returns_ByteContent_WhenFileIsParsed()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterService = new WordCounterService(logger.Object);
        var fileMock = FileFixture.SetupFile("big.txt");

        //Act
        var result = wordCounterService.GetWordCountByteContent(fileMock);

        //Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetWordCountByteContent_Returns_NoContent_WhenFileCannotBeParsed()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterService = new WordCounterService(logger.Object);
        var fileMock = new Mock<IFormFile>().Object;
        var empty = new byte[123, 125];

        //Act
        var result = wordCounterService.GetWordCountByteContent(fileMock);

        //Assert
        result.Equals(empty);
    }
}