using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.DAL.Repository.Interfaces;

public interface ISubdivisionRepository : IBasicRepository<Subdivision>
{
    Task<Subdivision> CreateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default);
    Task DeleteSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default);
    Task UpdateSubdivisionAsync(Subdivision subdivision, CancellationToken cancellationToken = default);
}