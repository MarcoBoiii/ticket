using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Dto;
using System.Runtime.CompilerServices;
namespace API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
    {

        var existingUser = await _userService.GetUserByEmailAsync(dto.Email);
        if (existingUser != null)
        {
            return Conflict("Ein Benutzer mit dieser E-Mail-Adresse existiert bereits.");
        }

        await _userService.CreateUser(dto);
        return Ok();
    }

    [HttpPost("getUserByEmail")]
    public async Task<IActionResult> GetUserByEmailAsync([FromBody] UserDto dto)
    {
        var result = await _userService.GetUserByEmailAsync(dto.Email);
        return Ok(result);
    }

    [HttpPost("logInUser")]
    public async Task<Guid> LogInUser([FromBody] UserDto dto)
    {
        var result = await _userService.GetUserAsync(dto.Email, dto.Password);
        if ( result is not null )
            return result.Id;
        else
            return Guid.Empty;
    }

    [HttpPost("updateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto dto)
    {
        await _userService.UpdateUser(dto);
        return Ok();
    }

    [HttpGet("deleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUserById(id);
        return Ok();
    }

    [HttpGet("getUser/{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var result = await _userService.GetUserById(id);
        return Ok(result);
    }

    [HttpGet("getUserName/{id}")]
    public async Task<IActionResult> GetUserName(Guid id)
    {
        var result = await _userService.GetUserNameById(id);
        return Ok(new
        {
            result.Value.FirstName,
            result.Value.LastName
        });
    }
}
