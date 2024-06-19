namespace OutOfOffice.BLL.Exceptions;

public class AbsenceReasonNotFoundException : CustomException
{
    public AbsenceReasonNotFoundException(string message) : base(message)
    {
    }
}