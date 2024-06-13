namespace OutOfOffice.BLL.Exceptions;

public class ProjectNotFoundException : CustomException
{
    public ProjectNotFoundException(string message) : base(message)
    {
    }
}