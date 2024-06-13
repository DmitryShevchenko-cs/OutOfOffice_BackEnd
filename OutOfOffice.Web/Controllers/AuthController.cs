using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Exceptions;
using OutOfOffice.BLL.Models.Employees;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.DAL.Entity.Employees;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Helpers;
using OutOfOffice.Web.Models;
using OutOfOffice.Web.Models.Enums;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TokenHelper _tokenHelper;
    private readonly IAuthEmployeeService _authEmployeeService;
    private readonly IMapper _mapper;
    public AuthController(TokenHelper tokenHelper, IAuthEmployeeService authEmployeeService, IMapper mapper)
    {
        _tokenHelper = tokenHelper;
        _authEmployeeService = authEmployeeService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> AuthorizeEmployee([FromBody] AuthorizeModel model, CancellationToken cancellationToken)
    {
        var employee = await _authEmployeeService.GetByLoginAndPasswordAsync(model.Login, model.Password, cancellationToken);
        var token = _tokenHelper.GetToken(employee.Id);
        var refreshToken = TokenHelper.GenerateRefreshToken(token);
        DateTime? expiredDate = model.IsNeedToRemember ? null : DateTime.Now;

        await _authEmployeeService.AddAuthorizationValueAsync(
            employee,
            refreshToken,
            expiredDate,
            cancellationToken);
        
        return Ok(new { accessKey = token, refresh_token = refreshToken, expiredDate = expiredDate });
    }
    
    [AllowAnonymous]
    [HttpPost("token/{refreshToken}")]
    public async Task<IActionResult> UpdateTokenAsync([FromQuery] string refreshToken, CancellationToken cancellationToken)
    {
        refreshToken = refreshToken.Replace(" ", "+"); 
        var user = await _authEmployeeService.GetUserByRefreshTokenAsync(refreshToken, cancellationToken);
        var token = _tokenHelper.GetToken(user.Id);
        return Ok(new { accessKey = token, refresh_token = refreshToken, expiredDate = user.AuthorizationInfo!.ExpiredDate });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> LogOutAsync(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        await _authEmployeeService.LogOutAsync(userId, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var userModel = await _authEmployeeService.GetUserById(userId, cancellationToken);
        
        var currentUser = _mapper.Map<CurrentUserViewModel>(userModel);
        currentUser.UserType = userModel switch
        {
            HrManager => UserType.HrManager,

            ProjectManager => UserType.ProjectManager,

            Employee => UserType.Employee,

            Admin => UserType.Admin,

            _ => throw new EmployeeNotFoundException($"User with Id {userId} not found")
        };
        return Ok(currentUser);
    }

}