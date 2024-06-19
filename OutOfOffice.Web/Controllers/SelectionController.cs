using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Selections;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class SelectionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAbsenceReasonService _absenceReasonService;
    private readonly IProjectTypeService _projectTypeService;
    private readonly ISubdivisionService _subdivisionService;
    private readonly IPositionService _positionService;

    public SelectionController(IMapper mapper, IAbsenceReasonService absenceReasonService, IProjectTypeService projectTypeService, ISubdivisionService subdivisionService, IPositionService positionService)
    {
        _mapper = mapper;
        _absenceReasonService = absenceReasonService;
        _projectTypeService = projectTypeService;
        _subdivisionService = subdivisionService;
        _positionService = positionService;
    }

    [HttpGet("absence-reasons")]
    public async Task<IActionResult> GetAbsenceReasons(CancellationToken cancellationToken = default)
    {
        var absenceReasons = await _absenceReasonService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<AbsenceReasonViewModel>>(absenceReasons));
    }
    
    [HttpGet("project-type")]
    public async Task<IActionResult> GetProjectTypes(CancellationToken cancellationToken = default)
    {
        var absenceReasons = await _projectTypeService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<SelectionViewModel>>(absenceReasons));
    }
    
    [HttpGet("subdivision")]
    public async Task<IActionResult> GetSubdivisions(CancellationToken cancellationToken = default)
    {
        var subdivisions = await _subdivisionService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<SelectionViewModel>>(subdivisions));
    }
    
    [HttpGet("position")]
    public async Task<IActionResult> GetPositions(CancellationToken cancellationToken = default)
    {
        var positions = await _positionService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<SelectionViewModel>>(positions));
    }
    
}