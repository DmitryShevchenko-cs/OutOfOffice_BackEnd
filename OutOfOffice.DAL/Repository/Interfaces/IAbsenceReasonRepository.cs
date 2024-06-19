using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IAbsenceReasonRepository : IBasicRepository<AbsenceReason>
{
    Task<AbsenceReason> CreateAbsenceReason(AbsenceReason request, CancellationToken cancellationToken = default);
    Task DeleteAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default);
    Task UpdateAbsenceReasonAsync(AbsenceReason request, CancellationToken cancellationToken = default);
}