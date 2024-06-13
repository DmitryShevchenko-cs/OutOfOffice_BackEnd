using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Models;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public ProjectController(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody]ProjectCreateModel projectCreateModel, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var project = await _projectService.CreateProjectAsync(userId, _mapper.Map<ProjectModel>(projectCreateModel), cancellationToken);
        return Ok(_mapper.Map<ProjectViewModel>(project));
    }
    
    [HttpDelete("{projectId:int}")]
    public async Task<IActionResult> DeleteProject(int projectId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _projectService.DeleteProjectAsync(projectId, userId, cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProject([FromBody]ProjectUpdateModel projectCreateModel, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var project= await _projectService.UpdateProjectAsync(userId,_mapper.Map<ProjectModel>(projectCreateModel), cancellationToken);
        return Ok(_mapper.Map<ProjectViewModel>(project));
    }
    
    [HttpPut("deactivate/{projectId:int}")]
    public async Task<IActionResult> DeactivateProject(int projectId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _projectService.DeactivateProjectAsync(projectId, userId, cancellationToken);
        return Ok();
    }
    
    [HttpPut("employees")]
    public async Task<IActionResult> AddEmployeesProject([FromBody]AddEmployeesModel addEmployeesModel, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _projectService.AddEmployeesInProjectAsync(userId, addEmployeesModel.ProjectId, addEmployeesModel.EmployeesIds, cancellationToken);
        return Ok();
    }

    [HttpGet("project-manager")]
    public async Task<IActionResult> GetAllByProjectManager(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var projects = await _projectService.GetAllByProjectManagerIdAsync(userId, cancellationToken);
        return Ok(_mapper.Map<List<ProjectViewModel>>(projects));
    }
    
    [HttpGet("hr-manager")]
    public async Task<IActionResult> GetAllByHrManager(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var projects = await _projectService.GetAllByHrManagerIdAsync(userId, cancellationToken);
        return Ok(_mapper.Map<List<ProjectViewModel>>(projects));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllByAdminManager(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var projects = await _projectService.GetAllAsync(userId, cancellationToken);
        return Ok(_mapper.Map<List<ProjectViewModel>>(projects));
    }

}