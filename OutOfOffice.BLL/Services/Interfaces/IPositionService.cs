using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IPositionService : IBasicService<Position>
{
    Task<List<Position>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<Position> Create(int managerId, string positionName,
        CancellationToken cancellationToken = default);
    Task Delete(int managerId, int positionId,
        CancellationToken cancellationToken = default);
    Task Update(int managerId, Position position,
        CancellationToken cancellationToken = default);
}