using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Word.Counter.Api.Controllers;
using Word.Counter.Api.Models;
using Word.Counter.Api.Services;
using Word.Counter.Api.Tests.Fixtures;

namespace Word.Counter.Api.Tests.Controllers;

public class WordCounterControllerTests
{
    [Fact]
    public void GetWordCountFile_Returns_BadRequest_WhenFileIsNotRightExtension()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterController = new WordCounterController();
        var fileValidatorService = new FileValidatorService();
        var wordCounterService = new WordCounterService(logger.Object);
        var mockFile = FileFixture.SetupFileWithExtension(".pdf", 1);
        var fileUploadModel = new FileUploadModel
        {
            File = mockFile
        };

        //Act
        var result = (BadRequestObjectResult)wordCounterController.GetWordCountFile(fileValidatorService,
            wordCounterService,
            fileUploadModel);

        //Assert
        result.StatusCode.Should().Be(400);
    }


    [Fact]
    public void GetWordCountFile_Returns_BadRequest_WhenFileLengthIs0()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterController = new WordCounterController();
        var fileValidatorService = new FileValidatorService();
        var wordCounterService = new WordCounterService(logger.Object);
        var mockFile = FileFixture.SetupFileWithExtension(".txt");
        var fileUploadModel = new FileUploadModel
        {
            File = mockFile
        };

        //Act
        var result = (BadRequestObjectResult)wordCounterController.GetWordCountFile(fileValidatorService,
            wordCounterService,
            fileUploadModel);

        //Assert
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public void GetWordCountFile_Returns_BadRequest_WhenFileIsNull()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterController = new WordCounterController();
        var fileValidatorService = new FileValidatorService();
        var wordCounterService = new WordCounterService(logger.Object);
        var mockFile = new Mock<IFormFile>().Object;
        var fileUploadModel = new FileUploadModel
        {
            File = mockFile
        };

        //Act
        var result = (BadRequestObjectResult)wordCounterController.GetWordCountFile(fileValidatorService,
            wordCounterService,
            fileUploadModel);

        //Assert
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public void GetWordCountFile_Returns_Ok_WhenFileIsCorrect()
    {
        //Arrange
        var logger = new Mock<ILogger<WordCounterService>>();
        var wordCounterController = new WordCounterController();
        var fileValidatorService = new FileValidatorService();
        var wordCounterService = new WordCounterService(logger.Object);
        var mockFile = FileFixture.SetupFile("big.txt");

        var fileUploadModel = new FileUploadModel
        {
            File = mockFile
        };

        //Act
        var result = (FileContentResult)wordCounterController.GetWordCountFile(fileValidatorService,
            wordCounterService,
            fileUploadModel);

        //Assert
        result.Should().NotBeNull();
    }
}
