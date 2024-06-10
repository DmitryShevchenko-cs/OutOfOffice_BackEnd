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
    
    
}