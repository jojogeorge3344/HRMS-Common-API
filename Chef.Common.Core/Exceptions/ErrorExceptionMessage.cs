namespace Chef.Common.Core;

public class ErrorExceptionMessage
{
    public ErrorExceptionMessage(int code, string msg)
    {
        Message = msg;
        Code = code;
    }

    public string Message { get; set; }

    public int Code { get; set; }
}
