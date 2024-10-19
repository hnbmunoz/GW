using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.Azure;
using MLAB.PlayerEngagement.Core.Models.Azure.Request;
using MLAB.PlayerEngagement.Core.Models.Azure.Response;
using MLAB.PlayerEngagement.Infrastructure.Config;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AzureController : ControllerBase
{
    private readonly IOptions<ConnectionString> _config;
    private readonly Core.Logging.ILogger<UploadController> _logger;
    public AzureController(IOptions<ConnectionString> config, Core.Logging.ILogger<UploadController> logger)
    {
        _config = config;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> UploadImage()
    {
        try
        {

            var postedFile = HttpContext.Request.Form.Files["file"];

            if (postedFile != null)
            {
                _logger.LogInfo("UploadImage | File received");
                // Create or retrieve the CloudBlobContainer
                var container = GetBlobContainerClient();

                // Create a unique name for the blob to avoid overwrites
                var uniqueFileName = $"{Guid.NewGuid().ToString()}_{postedFile.FileName}";
                var blobName = $"{_config.Value.ContainerName}\\{uniqueFileName}";

                // Retrieve reference to a blob
                var blockBlob = container.GetBlobClient(blobName);

                // Upload the file
                using (var stream = postedFile.OpenReadStream())
                {
                    await blockBlob.UploadAsync(stream);
                }
                // Get the URL of the uploaded blob               
                _logger.LogInfo("UploadImage | Success");
                return Ok(new { uri = uniqueFileName });
            }
            _logger.LogInfo("UploadImage | No file received");
            return BadRequest("No file received");
        }
        catch (Exception ex)
        {
            _logger.LogError($"UploadImage | Exception | {ex}");
            return BadRequest(ex);
        }
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetImages([FromBody] IEnumerable<FileListRequestModel> fileNames)
    {
        try
        {
            _logger.LogInfo($"GetImages | Retrieving images from fileNames.");

            var container = GetBlobContainerClient();
            var base64Strings = new List<FileListResponseModel>();

            foreach (var file in fileNames)
            {
                _logger.LogInfo($"GetImages | Retrieving image from fileNames: {fileNames}");

                var blobName = $"{_config.Value.ContainerName}\\{file.FileName}"; // You might need to adjust this depending on your requirements
                var blobClient = container.GetBlobClient(blobName);

                if (!await blobClient.ExistsAsync())
                {
                    _logger.LogError($"Blob not found at fileName: {file.FileName}");
                    continue; // Skip this fileName and move to the next one
                }

                using (var memoryStream = new MemoryStream())
                {
                    await blobClient.DownloadToAsync(memoryStream);
                    var byteArray = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(byteArray);
                    var result = new FileListResponseModel()
                    {
                        FileName = file.FileName,
                        OriginalFileName = file.OriginalFileName,
                        Base64Text = base64String
                    };
                    base64Strings.Add(result);
                }
            }

            return Ok(base64Strings);
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetImages | Exception | {ex}");
            return BadRequest(ex.Message);

        }
    }
    private BlobContainerClient GetBlobContainerClient()
    {
        var blobClient = new BlobServiceClient(new Uri(_config.Value.StorageConnectionString));
        var container = blobClient.GetBlobContainerClient(_config.Value.ContainerName);

        return container;
    }
}
