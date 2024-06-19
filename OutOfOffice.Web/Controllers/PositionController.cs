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
public class PositionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPositionService _positionService;

    public PositionController(IMapper mapper, IPositionService positionService)
    {
        _mapper = mapper;
        _positionService = positionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPositions(CancellationToken cancellationToken = default)
    {
        var positions = await _positionService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<SelectionViewModel>>(positions));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SelectionRequest position,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var positions = await _positionService.Create(userId, position.Name, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(positions));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SelectionViewModel position,
        CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _positionService.Update(userId, _mapper.Map<Position>(position), cancellationToken);
        return Ok();
    }

    [HttpDelete("{positionId:int}")]
    public async Task<IActionResult> Delete(int positionId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _positionService.Delete(userId, positionId, cancellationToken);
        return Ok();
    }

    [HttpGet("{positionId:int}")]
    public async Task<IActionResult> GetById(int positionId, CancellationToken cancellationToken = default)
    {
        var position = await _positionService.GetByIdAsync(positionId, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(position));
    }
}