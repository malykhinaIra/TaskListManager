using Microsoft.AspNetCore.Mvc;
using TestTask1.Application.DataTransferObjects.Requests;
using TestTask1.Application.Services;

namespace TestTask1.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _userService.UserExistsAsync(request.UserId))
        {
            return BadRequest("User with this ID already exists");
        }

        await _userService.CreateUserAsync(request);
        
        return new OkResult();
    }
}