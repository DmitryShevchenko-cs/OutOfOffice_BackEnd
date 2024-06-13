namespace OutOfOffice.BLL.Exceptions;

public class ApprovalRequestNotFoundException : CustomException
{
    public ApprovalRequestNotFoundException(string message) : base(message)
    {
    }
}