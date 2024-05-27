namespace СontractAccountingSystem.Server.Features;

public class RequestResult
{
    public bool Success { get; set; }
    public string Message { get; set; }

    public RequestResult(bool success)
    {
        Success = success;
    }

    public RequestResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
