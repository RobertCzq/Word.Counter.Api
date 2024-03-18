using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using Word.Counter.Api.Services;
using Word.Counter.Api.Tests.Fixtures;

namespace Word.Counter.Api.Tests.Services;

public class WordCounterServiceTests
{
    #region SanityChecks

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

    #endregion

    #region StressTest

    [Fact]
    public void GetWordCountByteContent_Returns_ByteContent_WhenBiggFileIsParsed()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterService = new WordCounterService(logger.Object);
        var fileMock = FileFixture.SetupFile("100MB.txt");

        //Act
        var result = wordCounterService.GetWordCountByteContent(fileMock);

        //Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GetWordCountByteContent_Returns_ByteContent_WhenBigFileIsParsed()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterService = new WordCounterService(logger.Object);
        var fileMock = FileFixture.SetupFile("100mb-examplefile-com.txt");

        //Act
        var result = wordCounterService.GetWordCountByteContent(fileMock);

        //Assert
        result.Should().NotBeNullOrEmpty();
    }

    #endregion

    #region CountIsRight

    [Fact]
    public void GetWordCountByteContent_Returns_RightCount_WhenFileIsParsed()
    {
        //Arrange
        var whatResultShouldBe = "{\"This\":{ \"Count\":2},\"is\":{\"Count\":2},\"a\":{\"Count\":2},\"test\":{\"Count\":2}}";

        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterService = new WordCounterService(logger.Object);
        var fileMock = FileFixture.SetupFile("MultipleSeparators.txt");

        //Act
        var result = wordCounterService.GetWordCountByteContent(fileMock);
        var stringValue = Encoding.Default.GetString(result);

        //Assert
        stringValue.Equals(whatResultShouldBe);
    }


    #endregion
}