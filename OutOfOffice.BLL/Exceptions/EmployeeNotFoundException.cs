namespace OutOfOffice.BLL.Exceptions;

public class EmployeeNotFoundException : CustomException
{
    public EmployeeNotFoundException(string message) : base(message)
    {
    }
}