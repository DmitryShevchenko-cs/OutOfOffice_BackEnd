namespace OutOfOffice.BLL.Exceptions;

public class PositionNotFoundException : CustomException
{
    public PositionNotFoundException(string message) : base(message)
    {
    }
}