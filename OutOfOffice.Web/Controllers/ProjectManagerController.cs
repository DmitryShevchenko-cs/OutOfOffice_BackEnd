using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectManagerController : ControllerBase
{
    private readonly IGeneralEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public ProjectManagerController(IGeneralEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateManager([FromBody] EmployeeCreateModel manager, CancellationToken cancellationToken)
    {
        await _employeeService.CreateEmployeeAsync(_mapper.Map<GeneralEmployeeModel>(manager), cancellationToken);

        return Ok(manager);
    }
    
}