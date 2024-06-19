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
public class SubdivisionController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISubdivisionService _subdivisionService;

    public SubdivisionController(IMapper mapper, ISubdivisionService subdivisionService)
    {
        _mapper = mapper;
        _subdivisionService = subdivisionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSubdivisions(CancellationToken cancellationToken = default)
    {
        var subdivisions = await _subdivisionService.GetAllAsync(cancellationToken);
        return Ok(_mapper.Map<List<SelectionViewModel>>(subdivisions));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SelectionRequest subdivision, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var positions = await _subdivisionService.Create(userId, subdivision.Name, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(positions));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SelectionViewModel subdivision, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _subdivisionService.Update(userId, _mapper.Map<Subdivision>(subdivision), cancellationToken);
        return Ok();
    }
    [HttpDelete("{subdivisionId:int}")]
    public async Task<IActionResult> Delete(int subdivisionId, CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        await _subdivisionService.Delete(userId, subdivisionId, cancellationToken);
        return Ok();
    }
    [HttpGet("{subdivisionId:int}")]
    public async Task<IActionResult> GetById(int subdivisionId, CancellationToken cancellationToken = default)
    {
        var subdivision = await _subdivisionService.GetByIdAsync(subdivisionId, cancellationToken);
        return Ok(_mapper.Map<SelectionViewModel>(subdivision));
    }
}