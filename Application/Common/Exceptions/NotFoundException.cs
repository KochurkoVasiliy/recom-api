namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public const int ErrorCode = 1004;

    public string EntityName { get; }
    public object Key { get; }
    public int Code { get; }

    public NotFoundException(string entityName, object key)
        : base($"Entity \"{entityName}\" ({key}) not found.")
    {
        EntityName = entityName;
        Key = key;
        Code = ErrorCode;
    }

    public NotFoundException(string entityName, object key, string message)
        : base(message)
    {
        EntityName = entityName;
        Key = key;
        Code = ErrorCode;
    }

    public NotFoundException(string entityName, object key, string message, Exception innerException)
        : base(message, innerException)
    {
        EntityName = entityName;
        Key = key;
        Code = ErrorCode;
    }

    public override string ToString()
    {
        return $"Error Code: {Code}, Entity: {EntityName}, Key: {Key}, Message: {Message}, InnerException: {InnerException}";
    }
}