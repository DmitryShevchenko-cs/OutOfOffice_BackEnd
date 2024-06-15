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
public class HrManagerController : ControllerBase
{
    private readonly IManagerService _managerService;
    private readonly IMapper _mapper;
    
    
    public HrManagerController(IManagerService managerService, IMapper mapper)
    {
        _managerService = managerService;
        _mapper = mapper;
    }


}