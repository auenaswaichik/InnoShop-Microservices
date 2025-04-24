using System.Threading.Tasks;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace WebApi.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase {

    private readonly IUserService _service;
    public UserController(IUserService service) =>
        _service = service;

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<ActionResult> DeleteUser(Guid id) {

        await _service.RemoveUser(id);
        return Ok("User was seccesfully removed");

    }
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireAdminRole")]
    [Authorize(Policy = "RequireClientRole")]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDTO updateUser, Guid id) {
        var userDTO = await _service.UpdateUser(id, updateUser);
        return Ok("User was succesfully updated: " + userDTO);
    }
}