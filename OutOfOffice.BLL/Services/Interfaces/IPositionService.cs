using OutOfOffice.DAL.Entity.Selections;

namespace OutOfOffice.BLL.Services.Interfaces;

public interface IPositionService : IBasicService<Position>
{
    Task<List<Position>> GetAllAsync(CancellationToken cancellationToken);
}