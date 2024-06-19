using Microsoft.EntityFrameworkCore;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.BLL.Services;

public class ProjectTypeService : IProjectTypeService
{
    private readonly IProjectTypeRepository _projectTypeRepository;

    public ProjectTypeService(IProjectTypeRepository projectTypeRepository)
    {
        _projectTypeRepository = projectTypeRepository;
    }


    public async Task<ProjectType> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var projectType = await _projectTypeRepository.GetByIdAsync(id, cancellationToken);
        if (projectType is null)
            throw new ProjectTypeNotFoundException($"Absence reason with id {id} not found");

        return projectType;
    }

    public async Task<List<ProjectType>> GetAllAsync(CancellationToken cancellationToken)
    {
        var projectType = await _projectTypeRepository.GetAll().ToListAsync(cancellationToken);
        return projectType;
    }
}