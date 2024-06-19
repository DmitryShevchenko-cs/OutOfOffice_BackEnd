namespace OutOfOffice.BLL.Exceptions;

public class ProjectTypeNotFoundException : CustomException
{
    public ProjectTypeNotFoundException(string message) : base(message)
    {
    }
}