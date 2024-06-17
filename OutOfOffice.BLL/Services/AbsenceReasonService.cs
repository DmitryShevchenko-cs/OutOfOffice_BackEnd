using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class AbsenceReasonService : IAbsenceReasonService
{
    private readonly IAbsenceReasonRepository _absenceReasonRepository;

    public AbsenceReasonService(IAbsenceReasonRepository absenceReasonRepository)
    {
        _absenceReasonRepository = absenceReasonRepository;
    }

    public async Task<AbsenceReason> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var absenceReason = await _absenceReasonRepository.GetByIdAsync(id, cancellationToken);
        if (absenceReason is null)
            throw new AbsenceReasonNotFoundException($"Absence reason with id {id} not found");

        return absenceReason;
    }

    public async Task<List<AbsenceReason>> GetAllAsync(CancellationToken cancellationToken)
    {
        var absenceReason = await _absenceReasonRepository.GetAll().ToListAsync(cancellationToken);
        return absenceReason;
    }
}