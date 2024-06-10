using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.BLL.Services.Interfaces;
using OutOfOffice.Web.Extensions;
using OutOfOffice.Web.Helpers;
using OutOfOffice.Web.Models;

namespace OutOfOffice.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TokenHelper _tokenHelper;
    private readonly IAuthEmployeeService _authEmployeeService;

    public AuthController(TokenHelper tokenHelper, IAuthEmployeeService authEmployeeService)
    {
        _tokenHelper = tokenHelper;
        _authEmployeeService = authEmployeeService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> AuthorizeEmployee([FromBody] EmployeeAuthorizeModel model, CancellationToken cancellationToken)
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
    
    [HttpPost]
    [Route(("logout"))]
    public async Task<IActionResult> LogOutAsync(CancellationToken cancellationToken)
    {
        var employeeId = User.GetUserId();
        await _authEmployeeService.LogOutAsync(employeeId, cancellationToken);
        return Ok();
    }

}