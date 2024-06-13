namespace OutOfOffice.BLL.Exceptions;

public class ManagerNotFoundException : CustomException
{
    public ManagerNotFoundException(string message) : base(message)
    {
    }
}