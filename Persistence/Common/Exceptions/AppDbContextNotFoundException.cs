namespace Persistence.Common.Exceptions;

public class AppDbContextNotFoundException : Exception
{
    public const string DefaultMessage = "AppDbContext not found.";
    public const int ErrorCode = 1001; // All codes from 1000 to 2000 are reserved for the Persistence layer.

    public int Code { get; }

    public AppDbContextNotFoundException() : base(DefaultMessage)
    {
        Code = ErrorCode;
    }

    public AppDbContextNotFoundException(string message) : base(message)
    {
        Code = ErrorCode;
    }

    public AppDbContextNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        Code = ErrorCode;
    }

    public AppDbContextNotFoundException(int code, string message) : base(message)
    {
        Code = code;
    }

    public AppDbContextNotFoundException(int code, string message, Exception innerException) : base(message,
        innerException)
    {
        Code = code;
    }

    public override string ToString()
    {
        return $"Error Code: {Code}, Message: {Message}, InnerException: {InnerException}";
    }
}