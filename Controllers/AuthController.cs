using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Utils;

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
    public async Task<ActionResult> LogIn([FromBody] AccountLoginBind body)
    {
        var existingAccount = this._accountService.GetAccountByUsername(body.Username);

        if (existingAccount == null || existingAccount.Password != HashHelper.Hash(body.Password))
        {
            return NotFound(new { message = $"Invalid username or password" });
        }

        var claimsIdentity = new ClaimsIdentity(new List<Claim>{
            new Claim(ClaimTypes.Sid ,existingAccount.Id),
            new Claim(ClaimTypes.Role ,existingAccount.Role),
        }, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var cookieProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = true,
            ExpiresUtc = DateTime.Now.AddDays(1)
        };

        await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, cookieProperties);

        var accountDetail = new AccountDTO()
        {
            Id = existingAccount.Id,
            Username = existingAccount.Username,
            FirstName = existingAccount.FirstName,
            LastName = existingAccount.LastName,
            BirthDate = existingAccount.BirthDate,
            Role = existingAccount.Role,
            IsBanned = existingAccount.IsBanned
        };
        return Ok(accountDetail);
    }

    [HttpGet("logout")] // GET api/Auth/logout
    public async Task<ActionResult> LogOut()
    {
        await Request.HttpContext.SignOutAsync();

        return NoContent();
    }
}