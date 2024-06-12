namespace OutOfOffice.BLL.Exceptions;

public class LeaveRequestNotFoundException : CustomException
{
    public LeaveRequestNotFoundException(string message) : base(message)
    {
    }
}