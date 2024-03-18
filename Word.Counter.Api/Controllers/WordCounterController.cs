using Microsoft.AspNetCore.Mvc;
using Word.Counter.Api.Models;
using Word.Counter.Api.Services;

namespace Word.Counter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordCounterController : ControllerBase
    {
        [HttpPost("/GetWordCountFile"), DisableRequestSizeLimit]
        public IActionResult GetWordCountFile([FromServices] IFileValidatorService fileValidatorService,
            [FromServices] IWordCounterService wordCounterService,
            [FromForm] FileUploadModel fileUploadModel)
        {
            if (fileUploadModel.File is null || fileUploadModel.File.Length == 0)
            {
                return BadRequest("The file is null");
            }

            if (!fileValidatorService.IsFileExtensionAllowed(fileUploadModel.File, [".txt"]))
            {
                return BadRequest("Invalid file type. Please upload a .txt file");
            }

            var byteContent = wordCounterService.GetWordCountByteContent(fileUploadModel.File);

            return File(byteContent, "application/octet-stream", "wordcount.txt"); ;
        }
    }
}
