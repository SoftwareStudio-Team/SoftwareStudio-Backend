using Microsoft.AspNetCore.Mvc;

using Backend.Models;
using Backend.Services;

namespace Backend.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AuthController(IAccountService accountService)
    {
        this._accountService = accountService;
    }

    [HttpPost("login")] // POST api/Auth/login
    public ActionResult LogIn([FromBody] LoginForm loginForm)
    {
        /* implement login logic here */
        return NoContent();
    }

    [HttpGet("logout")] // GET api/Auth/logout
    public ActionResult LogOut()
    {
        /* implement logout logic here */
        return NoContent();
    }
}