namespace OutOfOffice.BLL.Exceptions;

public class OutOfBalanceLimitException : CustomException
{
    public OutOfBalanceLimitException(string message) : base(message)
    {
    }
}