namespace TaskManager.Communication.DTOs.Tasks.Response;

public class ResponseErrorJson
{
    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public List<string> ErrorMessages { get; set; }
}