using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IGeneralEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeeController(IGeneralEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }
    
    //hr manager or admin
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody]EmployeeCreateModel employeeCreateModel, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employee = await _employeeService.CreateEmployeeAsync(managerId,_mapper.Map<EmployeeModel>(employeeCreateModel), cancellationToken);
        return Ok(_mapper.Map<EmployeeViewModel>(employee));
    }
    
    //only HR or Admin
    [HttpGet]
    public async Task<IActionResult> GetEmployee(CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employees = await _employeeService.GetEmployeesAsync(managerId, cancellationToken);
        return Ok(_mapper.Map<List<EmployeeViewModel>>(employees));
    }
    
    [HttpPut("{employeeId:int}")]
    public async Task<IActionResult> DeactivateEmployee(int employeeId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await _employeeService.DeactivateEmployeeAsync(employeeId, cancellationToken);
        return Ok();
    }
    
    
}