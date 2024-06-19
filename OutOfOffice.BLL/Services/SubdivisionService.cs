using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class SubdivisionService : ISubdivisionService
{
    private readonly ISubdivisionRepository _subdivisionRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public SubdivisionService(ISubdivisionRepository subdivisionRepository, IEmployeeRepository employeeRepository)
    {
        _subdivisionRepository = subdivisionRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Subdivision> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var subdivision = await _subdivisionRepository.GetByIdAsync(id, cancellationToken);
        if (subdivision is null)
            throw new SubdivisionException($"Absence reason with id {id} not found");

        return subdivision;
    }

    public async Task<List<Subdivision>> GetAllAsync(CancellationToken cancellationToken)
    {
        var subdivision = await _subdivisionRepository.GetAll().ToListAsync(cancellationToken);
        return subdivision;
    }

    public async Task<Subdivision> Create(int managerId, string subdivisionName,
        CancellationToken cancellationToken = default)
    {
        //get HrManager or Admin
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var subdivisionDb = await _subdivisionRepository.GetAll().Where(r => r.Name == subdivisionName)
            .SingleOrDefaultAsync(cancellationToken);
        if (subdivisionDb != null)
            throw new SubdivisionException($"Subdivision with name {subdivisionName} created already");

        var subdivision = await _subdivisionRepository.CreateSubdivisionAsync(new Subdivision
        {
            Name = subdivisionName,
        }, cancellationToken);

        return subdivision;
    }

    public async Task Delete(int managerId, int subdivisionId, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var subdivision = await _subdivisionRepository.GetByIdAsync(subdivisionId, cancellationToken);
        if (subdivision is null)
            throw new SubdivisionException($"Subdivision with id {subdivisionId} not found");

        await _subdivisionRepository.DeleteSubdivisionAsync(subdivision, cancellationToken);
    }

    public async Task Update(int managerId, Subdivision subdivision, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var subdivisionCheck = await _subdivisionRepository.GetAll().Where(r => r.Name == subdivision.Name)
            .SingleOrDefaultAsync(cancellationToken);
        if (subdivisionCheck != null)
            throw new SubdivisionException($"Subdivision with name {subdivision.Name} created already");
        
        var subdivisionDb = await _subdivisionRepository.GetByIdAsync(subdivision.Id, cancellationToken);
        if (subdivisionDb is null)
            throw new SubdivisionException($"Subdivision with id {subdivision.Id} not found");

        subdivisionDb.Name = subdivision.Name;

        await _subdivisionRepository.UpdateSubdivisionAsync(subdivisionDb, cancellationToken);
    }
}