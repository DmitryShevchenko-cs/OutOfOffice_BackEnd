using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface ISubdivisionService : IBasicService<Subdivision>
{
    Task<List<Subdivision>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<Subdivision> Create(int managerId, string subdivisionName,
        CancellationToken cancellationToken = default);
    Task Delete(int managerId, int subdivisionId,
        CancellationToken cancellationToken = default);
    Task Update(int managerId, Subdivision subdivision,
        CancellationToken cancellationToken = default);
}