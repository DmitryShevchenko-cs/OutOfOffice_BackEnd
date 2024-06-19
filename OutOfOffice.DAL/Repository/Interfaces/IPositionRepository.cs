using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface IPositionRepository : IBasicRepository<Position>
{
    Task<Position> CreatePositionAsync(Position position, CancellationToken cancellationToken = default);
    Task DeletePositionAsync(Position position, CancellationToken cancellationToken = default);
    Task UpdatePositionAsync(Position position, CancellationToken cancellationToken = default);
}