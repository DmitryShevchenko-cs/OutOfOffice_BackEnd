using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectManagerController : ControllerBase
{
    private readonly IManagerRepository _managerRepository;
    private readonly IMapper _mapper;

    public ProjectManagerController(IManagerRepository managerRepository, IMapper mapper)
    {
        _managerRepository = managerRepository;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateManager([FromBody] ProjectManagerCreateModel manager, CancellationToken cancellationToken)
    {
        await _managerRepository.AddManagerAsync(_mapper.Map<ProjectManager>(manager), cancellationToken);

        return Ok(manager);
    }
    
}