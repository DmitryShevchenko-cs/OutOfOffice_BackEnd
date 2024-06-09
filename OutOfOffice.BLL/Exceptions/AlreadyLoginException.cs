namespace OutOfOffice.BLL.Exceptions;

public class AlreadyLoginException : CustomException
{
    public AlreadyLoginException(string message) : base(message)
    {
    }
}