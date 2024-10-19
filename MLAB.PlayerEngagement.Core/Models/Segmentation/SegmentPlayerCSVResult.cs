using Microsoft.AspNetCore.Mvc;

namespace MLAB.PlayerEngagement.Core.Models.Segmentation;

public class SegmentPlayerCSVResult : FileResult
{
    private readonly IEnumerable<SegmentPlayer> _playerData;
    public SegmentPlayerCSVResult(IEnumerable<SegmentPlayer> playerData, string fileDownloadName) : base("text/csv")
    {
        _playerData = playerData;
        FileDownloadName = fileDownloadName;
    }

    public async override Task ExecuteResultAsync(ActionContext context)
    {
        var response = context.HttpContext.Response;
        context.HttpContext.Response.Headers.Add("Content-Disposition", new[] { "attachment; filename=" + FileDownloadName });
        using (var streamWriter = new StreamWriter(response.Body))
        {
            await streamWriter.WriteLineAsync(
              $"Id, PlayerId, UserName, BrandName, CurrencyName, VipLevelName, AccountStatus, RegistrationDate"
            );
            foreach (var p in _playerData)
            {
                await streamWriter.WriteLineAsync(
                  $"{p.Id}, {p.PlayerId}, {p.UserName}, {p.BrandName}, {p.CurrencyName}, {p.VipLevelName}, {p.AccountStatus}, {p.RegistrationDate?.ToShortDateString()}"
                );
                await streamWriter.FlushAsync();
            }
            await streamWriter.FlushAsync();
        }
    }
}
