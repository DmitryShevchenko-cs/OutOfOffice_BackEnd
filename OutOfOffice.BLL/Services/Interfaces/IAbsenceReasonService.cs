using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IAbsenceReasonService : IBasicService<AbsenceReason>
{
    Task<List<AbsenceReason>> GetAllAsync(CancellationToken cancellationToken);

    Task<AbsenceReason> Create(int managerId, string absenceReasonDesc,
        CancellationToken cancellationToken = default);
    Task Delete(int managerId, int absenceReasonId,
        CancellationToken cancellationToken = default);
    Task Update(int managerId, AbsenceReason absenceReason,
        CancellationToken cancellationToken = default);
}