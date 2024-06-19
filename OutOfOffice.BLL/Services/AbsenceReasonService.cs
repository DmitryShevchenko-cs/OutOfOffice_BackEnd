using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class AbsenceReasonService : IAbsenceReasonService
{
    private readonly IAbsenceReasonRepository _absenceReasonRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AbsenceReasonService(IAbsenceReasonRepository absenceReasonRepository, IEmployeeRepository employeeRepository)
    {
        _absenceReasonRepository = absenceReasonRepository;
        _employeeRepository = employeeRepository;
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

    public async Task<AbsenceReason> Create(int managerId, string absenceReasonDesc, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var absenceReasonDb = await _absenceReasonRepository.GetAll().Where(r => r.ReasonDescription == absenceReasonDesc)
            .SingleOrDefaultAsync(cancellationToken);
        if (absenceReasonDb != null)
            throw new PositionException($"Absence reason with name {absenceReasonDesc} created already");

        var absenceReason = await _absenceReasonRepository.CreateAbsenceReason(new AbsenceReason()
        {
            ReasonDescription = absenceReasonDesc,
        }, cancellationToken);

        return absenceReason;
    }

    public async Task Delete(int managerId, int absenceReasonId, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var absenceReason = await _absenceReasonRepository.GetByIdAsync(absenceReasonId, cancellationToken);
        if (absenceReason is null)
            throw new PositionException($"Absence reason with id {absenceReasonId} not found");

        await _absenceReasonRepository.DeleteAbsenceReasonAsync(absenceReason, cancellationToken);
    }

    public async Task Update(int managerId, AbsenceReason absenceReason, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is HrManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Project manager or admin with Id {managerId} not found");

        var absenceReasonCheck = await _absenceReasonRepository.GetAll().Where(r => r.ReasonDescription == absenceReason.ReasonDescription)
            .SingleOrDefaultAsync(cancellationToken);
        if (absenceReasonCheck != null)
            throw new ProjectTypeException($"Absence reason with name {absenceReason.ReasonDescription} created already");
        
        var absenceReasonDb = await _absenceReasonRepository.GetByIdAsync(absenceReason.Id, cancellationToken);
        if (absenceReasonDb is null)
            throw new ProjectTypeException($"Absence reason with id {absenceReason.Id} not found");

        absenceReasonDb.ReasonDescription = absenceReason.ReasonDescription;

        await _absenceReasonRepository.UpdateAbsenceReasonAsync(absenceReasonDb, cancellationToken);

    }
}