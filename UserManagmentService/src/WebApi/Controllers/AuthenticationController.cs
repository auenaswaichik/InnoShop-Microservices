using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Infrastrucure.Services;
using Application.IServices;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase {

    private readonly IAuthenticationService _service;
    public AuthenticationController(IAuthenticationService service) =>
        _service = service;

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserDTO loginUser) {
        var token = await _service.AuthenticateUser(loginUser);
        if (token is null) 
            return Unauthorized();

        return Ok(new {Token = token});
    }
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDTO registerUser) {

        var success = await _service.RegistrateUser(registerUser);

        if (!success)
            return BadRequest("Username or email were already used, or password was not confirmed");

        return Ok("Registration was success");

    }

}