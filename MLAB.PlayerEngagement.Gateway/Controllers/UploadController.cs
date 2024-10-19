using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Infrastructure.Config;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IOptions<ConnectionString> _config;
    private readonly Core.Logging.ILogger<UploadController> _logger;
    public UploadController(IOptions<ConnectionString> config, Core.Logging.ILogger<UploadController> logger)
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
                var blobName = $"{_config.Value.ContainerName}\\{Guid.NewGuid().ToString()}_{postedFile.FileName}";
                
                // Retrieve reference to a blob
                var blockBlob = container.GetBlobClient(blobName);

                // Upload the file
                using (var stream = postedFile.OpenReadStream())
                {
                    await blockBlob.UploadAsync(stream);
                }
                // Get the URL of the uploaded blob               
                _logger.LogInfo("UploadImage | Success");
                return Ok(new { location = blockBlob.Uri });
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
    [HttpGet]
    public async Task<IActionResult> GetImage(string blobUrl)
    {
        try
        {
            _logger.LogInfo($"GetImage | Retrieving image from URL: {blobUrl}");

            var container = GetBlobContainerClient();

            var blobName = $"{_config.Value.ContainerName}\\2682a1f2-cfd9-4bd3-bcfc-07018d0157a4_3f071dc4-963b-56d1-3e17-7b7629c98ebc";
            // Retrieve reference to a blob
            var blobClient = container.GetBlobClient(blobName);

            // Check if the blob exists
            if (!await blobClient.ExistsAsync())
            {
                _logger.LogError($"Blob not found at URL: {blobUrl}");
                return NotFound();
            }

            // Download blob content into memory stream
            using (var memoryStream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(memoryStream);

                // Convert memory stream to byte array
                var byteArray = memoryStream.ToArray();

                // Convert byte array to Base64 string
                var base64String = Convert.ToBase64String(byteArray);

                // Return the Base64 string
                return Ok(base64String);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetImage | Exception | {ex}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> DownloadImage(string blobName)
    {
        try
        {
            _logger.LogInfo($"DownloadImage | Downloading image: {blobName}");

            // Retrieve the blob container
            var container = GetBlobContainerClient();

            // Get a reference to the blob
            var blobClient = container.GetBlobClient(blobName);

            // Check if the blob exists
            if (!await blobClient.ExistsAsync())
            {
                _logger.LogInfo($"DownloadImage | Blob '{blobName}' not found");
                return NotFound();
            }

            // Get the blob contents
            var blobDownloadInfo = await blobClient.DownloadAsync();

            // Convert blob content to base64
            var base64String = ""; // Convert.ToBase64String(blobDownloadInfo.Content.ToArray());

            // Return the base64 string
            return Ok(new { base64Image = base64String });
        }
        catch (Exception ex)
        {
            _logger.LogError($"DownloadImage | Exception | {ex}");
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
