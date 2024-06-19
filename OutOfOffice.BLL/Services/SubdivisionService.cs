using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class SubdivisionService : ISubdivisionService
{
    private readonly ISubdivisionRepository _subdivisionRepository;

    public SubdivisionService(ISubdivisionRepository subdivisionRepository)
    {
        _subdivisionRepository = subdivisionRepository;
    }

    public async Task<Subdivision> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var subdivision = await _subdivisionRepository.GetByIdAsync(id, cancellationToken);
        if (subdivision is null)
            throw new SubdivisionNotFoundException($"Absence reason with id {id} not found");

        return subdivision;
    }

    public async Task<List<Subdivision>> GetAllAsync(CancellationToken cancellationToken)
    {
        var subdivision = await _subdivisionRepository.GetAll().ToListAsync(cancellationToken);
        return subdivision;
    }
}