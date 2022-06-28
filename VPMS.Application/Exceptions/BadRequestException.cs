namespace VPMS.Application.Exceptions;

public class BadRequestException : ApplicationException
{
    public string PropertyName;

    public BadRequestException(string propertyName, string message) : base(message)
    {
        PropertyName = propertyName;
    }
}
