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

    public SelectionController(IMapper mapper, IAbsenceReasonService absenceReasonService)
    {
        _mapper = mapper;
        _absenceReasonService = absenceReasonService;
    }

    [HttpGet("Absence-reasons")]
    public async Task<IActionResult> GetAbsenceReason(CancellationToken cancellationToken = default)
    {
        var absenceReasons = await _absenceReasonService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<AbsenceReasonViewModel>>(absenceReasons));
    }
    
}