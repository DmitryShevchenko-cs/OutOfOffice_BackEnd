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
    
}