using Microsoft.AspNetCore.Http;
using Moq;

namespace Word.Counter.Api.Tests.Fixtures;

public static class FileFixture
{
    public static IFormFile SetupFile(string fn)
    {
        var fileMock = new Mock<IFormFile>();
        var filePath = Path.Combine(Environment.CurrentDirectory, @"TestFiles", fn);
        var physicalFile = new FileInfo(filePath);
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(physicalFile.OpenRead());
        writer.Flush();
        ms.Position = 0;
        var fileName = physicalFile.Name;
        //Setup mock file using info from physical file
        fileMock.Setup(_ => _.FileName).Returns(fileName);
        fileMock.Setup(_ => _.Length).Returns(ms.Length);
        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);

        return fileMock.Object;
    }

    public static IFormFile SetupFileWithExtension(string extension, int length = 0)
    {
        var fileMock = new Mock<IFormFile>();
        //Setup mock file using info from physical file
        fileMock.Setup(_ => _.FileName).Returns(string.Concat("Test", extension));
        fileMock.Setup(_ => _.Length).Returns(length);
        return fileMock.Object;
    }
}
