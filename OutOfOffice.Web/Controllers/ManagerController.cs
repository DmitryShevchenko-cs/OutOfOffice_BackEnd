using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.DAL.Repository.Interfaces;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ManagerController : ControllerBase
{
    private readonly IManagerService _managerService;
    private readonly IMapper _mapper;

    public ManagerController(IManagerService managerService, IMapper mapper)
    {
        _managerService = managerService;
        _mapper = mapper;
    }
    
    //admin has to authorize
    [HttpPost("project-manager")]
    public async Task<IActionResult> CreateProjectManager([FromBody] ManagerCreateModel manager, CancellationToken cancellationToken)
    {
        var adminId = User.GetUserId();
        var managerResult = await _managerService.CreateProjectManagerAsync(adminId,_mapper.Map<ProjectManagerModel>(manager), cancellationToken);
        return Ok(_mapper.Map<ManagerViewModel>(managerResult));
    }
    
    //admin has to authorize
    [HttpPost("hr-manager")]
    public async Task<IActionResult> CreateHrManager([FromBody] ManagerCreateModel manager, CancellationToken cancellationToken)
    {
        var adminId = User.GetUserId();
        var managerResult = await _managerService.CreateProjectManagerAsync(adminId,_mapper.Map<HrManagerModel>(manager), cancellationToken);
        return Ok(_mapper.Map<ManagerViewModel>(managerResult));
    }
    
    
    [HttpPut]
    public async Task<IActionResult> UpdateManager([FromBody] ManagerUpdateModel manager, CancellationToken cancellationToken)
    {
        var managerId = User.GetUserId();
        var updatedManager = await _managerService.UpdateManagerAsync(managerId,_mapper.Map<BaseManagerModel>(manager), cancellationToken);
        return Ok(_mapper.Map<ManagerViewModel>(updatedManager));
    }

    //only admin can delete managers
    [HttpDelete("{managerId:int}")]
    public async Task<IActionResult> DeleteManager(int managerId, CancellationToken cancellationToken = default)
    {
        var adminId = User.GetUserId();
        await _managerService.DeleteManagerAsync(adminId, managerId, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllManagers(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var managers = await _managerService.GetAll(userId, cancellationToken);
        return Ok(_mapper.Map<List<ManagerViewModel>>(managers));
    }
    
    [HttpGet("approvers")]
    public async Task<IActionResult> GetApprovers(CancellationToken cancellationToken = default)
    {
        var userId = User.GetUserId();
        var approvedRequest = await _managerService.GetApproversAsync(userId, cancellationToken);
        return Ok(_mapper.Map<List<ManagerViewModel>>(approvedRequest));
    }

}