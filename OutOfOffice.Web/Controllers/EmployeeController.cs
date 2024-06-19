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
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeService employeeService, IMapper mapper)
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
    
    [HttpGet]
    public async Task<IActionResult> GetEmployee(CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employees = await _employeeService.GetEmployeesAsync(managerId, cancellationToken);
        return Ok(_mapper.Map<List<EmployeeViewModel>>(employees));
    }
    
    [HttpGet("All")]
    public async Task<IActionResult> GetAllEmployee(CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employees = await _employeeService.GetAllAsync(managerId, cancellationToken);
        return Ok(_mapper.Map<List<EmployeeViewModel>>(employees));
    }
    
    [HttpGet("{employeeId:int}")]
    public async Task<IActionResult> GetEmployee(int employeeId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        var employees = await _employeeService.GetByIdAsync(employeeId, cancellationToken);
        return Ok(_mapper.Map<EmployeeFullViewModel>(employees));
    }
    
    [HttpPut("{employeeId:int}")]
    public async Task<IActionResult> DeactivateEmployee(int employeeId, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await _employeeService.DeactivateEmployeeAsync(employeeId, cancellationToken);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateModel employeeUpdateModel, CancellationToken cancellationToken = default)
    {
        var managerId = User.GetUserId();
        await _employeeService.UpdateEmployeeAsync(managerId, _mapper.Map<EmployeeModel>(employeeUpdateModel), cancellationToken);
        return Ok();
    }
}