using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public PositionService(IPositionRepository positionRepository, IEmployeeRepository employeeRepository)
    {
        _positionRepository = positionRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<Position> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var subdivision = await _positionRepository.GetByIdAsync(id, cancellationToken);
        if (subdivision is null)
            throw new PositionException($"Position with id {id} not found");

        return subdivision;
    }

    public async Task<List<Position>> GetAllAsync(CancellationToken cancellationToken)
    {
        var subdivision = await _positionRepository.GetAll().ToListAsync(cancellationToken);
        return subdivision;
    }

    public async Task<Position> Create(int managerId, string positionName, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var positionDb = await _positionRepository.GetAll().Where(r => r.Name == positionName)
            .SingleOrDefaultAsync(cancellationToken);
        if (positionDb != null)
            throw new PositionException($"Position with name {positionName} created already");

        var position = await _positionRepository.CreatePositionAsync(new Position()
        {
            Name = positionName,
        }, cancellationToken);

        return position;
    }

    public async Task Delete(int managerId, int positionId, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var position = await _positionRepository.GetByIdAsync(positionId, cancellationToken);
        if (position is null)
            throw new PositionException($"Position with id {positionId} not found");

        await _positionRepository.DeletePositionAsync(position, cancellationToken);

    }

    public async Task Update(int managerId, Position position, CancellationToken cancellationToken = default)
    {
        var managerDb = await _employeeRepository.GetAllManagers()
            .SingleOrDefaultAsync(r => r.Id == managerId && !(r is ProjectManager),
                cancellationToken);
        if (managerDb is null)
            throw new ManagerNotFoundException($"Hr manager or admin with Id {managerId} not found");

        var positionCheck = await _positionRepository.GetAll().Where(r => r.Name == position.Name)
            .SingleOrDefaultAsync(cancellationToken);
        if (positionCheck != null)
            throw new PositionException($"Position with name {position.Name} created already");
        
        var positionDb = await _positionRepository.GetByIdAsync(position.Id, cancellationToken);
        if (positionDb is null)
            throw new PositionException($"Position with id {position.Id} not found");

        positionDb.Name = position.Name;

        await _positionRepository.UpdatePositionAsync(positionDb, cancellationToken);

    }
}