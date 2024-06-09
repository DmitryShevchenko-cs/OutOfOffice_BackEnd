using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectManagerController : ControllerBase
{
    private readonly IManagerRepository _managerRepository;

    public ProjectManagerController(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateManager([FromBody] ProjectManager manager, CancellationToken cancellationToken)
    {
        await _managerRepository.AddManagerAsync(manager, cancellationToken);

        return Ok();
    }
    
}