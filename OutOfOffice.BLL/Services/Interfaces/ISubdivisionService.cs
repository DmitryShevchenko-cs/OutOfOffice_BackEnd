using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface ISubdivisionService : IBasicService<Subdivision>
{
    Task<List<Subdivision>> GetAllAsync(CancellationToken cancellationToken);
}