using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class SelectionController : ControllerBase
{
    private readonly IMapper _mapper;
    
}