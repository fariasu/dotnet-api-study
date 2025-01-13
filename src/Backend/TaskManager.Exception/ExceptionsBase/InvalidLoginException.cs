using System.Net;

namespace TaskManager.Exception.ExceptionsBase;

public class InvalidLoginException : TaskManagerException
{
    private const string InvalidEmailOrPassword = "Invalid username and/or password.";

    public InvalidLoginException() : base(InvalidEmailOrPassword)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}