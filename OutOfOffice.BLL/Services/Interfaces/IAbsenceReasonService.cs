using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IAbsenceReasonService : IBasicService<AbsenceReason>
{
    Task<List<AbsenceReason>> GetAllAsync(CancellationToken cancellationToken);
    
}