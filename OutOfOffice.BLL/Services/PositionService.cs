using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;

    public PositionService(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<Position> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var subdivision = await _positionRepository.GetByIdAsync(id, cancellationToken);
        if (subdivision is null)
            throw new PositionNotFoundException($"Absence reason with id {id} not found");

        return subdivision;
    }

    public async Task<List<Position>> GetAllAsync(CancellationToken cancellationToken)
    {
        var subdivision = await _positionRepository.GetAll().ToListAsync(cancellationToken);
        return subdivision;
    }
}