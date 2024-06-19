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
public class AbsenceReasonController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAbsenceReasonService _absenceReasonService;

    public AbsenceReasonController(IAbsenceReasonService absenceReasonService, IMapper mapper)
    {
        _absenceReasonService = absenceReasonService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAbsenceReasons(CancellationToken cancellationToken = default)
    {
        var absenceReasons = await _absenceReasonService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<AbsenceReasonViewModel>>(absenceReasons));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReasonDescriptionRequest reasonDescription,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var positions = await _absenceReasonService.Create(userId, reasonDescription.ReasonDescription, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(positions));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SelectionViewModel absenceReason,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _absenceReasonService.Update(userId, _mapper.Map<AbsenceReason>(absenceReason), cancellationToken);
        return Ok();
    }

    [HttpDelete("{absenceReasonId:int}")]
    public async Task<IActionResult> Delete(int absenceReasonId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _absenceReasonService.Delete(userId, absenceReasonId, cancellationToken);
        return Ok();
    }

    [HttpGet("{absenceReasonId:int}")]
    public async Task<IActionResult> GetById(int absenceReasonId, CancellationToken cancellationToken = default)
    {
        var absenceReason = await _absenceReasonService.GetByIdAsync(absenceReasonId, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(absenceReason));
    }
}