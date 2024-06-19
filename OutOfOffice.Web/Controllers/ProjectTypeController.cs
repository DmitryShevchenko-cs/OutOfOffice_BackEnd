using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class ProjectTypeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProjectTypeService _projectTypeService;

    public ProjectTypeController(IMapper mapper, IProjectTypeService projectTypeService)
    {
        _mapper = mapper;
        _projectTypeService = projectTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjectTypes(CancellationToken cancellationToken = default)
    {
        var absenceReasons = await _projectTypeService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<SelectionViewModel>>(absenceReasons));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SelectionRequest project,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var positions = await _projectTypeService.Create(userId, project.Name, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(positions));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SelectionViewModel projectType,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _projectTypeService.Update(userId, _mapper.Map<ProjectType>(projectType), cancellationToken);
        return Ok();
    }

    [HttpDelete("{projectTypeId:int}")]
    public async Task<IActionResult> Delete(int projectTypeId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _projectTypeService.Delete(userId, projectTypeId, cancellationToken);
        return Ok();
    }

    [HttpGet("{projectTypeId:int}")]
    public async Task<IActionResult> GetById(int projectTypeId, CancellationToken cancellationToken = default)
    {
        var projectType = await _projectTypeService.GetByIdAsync(projectTypeId, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(projectType));
    }
}