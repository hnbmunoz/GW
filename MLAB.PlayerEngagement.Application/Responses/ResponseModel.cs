using System.Net;

namespace MLAB.PlayerEngagement.Application.Responses;

public class ResponseModel
{
    public int Status { get; set; }
    public string Message { get; set; }

    public object Data { get; set; }

    public ResponseModel()
    {
        Status = (int)HttpStatusCode.OK;
        Message = HttpStatusCode.OK.ToString();
    }

    public ResponseModel(int status, string message)
    {
        Status = status;
        Message = message;
    }
}
