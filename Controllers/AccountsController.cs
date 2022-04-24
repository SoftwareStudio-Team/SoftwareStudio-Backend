using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Utils;

namespace Backend.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        this._accountService = accountService;
    }

    [HttpGet("{id}")] // GET api/Accounts/{id}
    public ActionResult<AccountDTO> GetDTOById(string id)
    {
        var existingAccount = this._accountService.GetDTOById(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        return existingAccount;
    }

    [HttpGet] // GET api/Accounts
    public ActionResult<List<AccountDTO>> GetAllDTO()
    {
        var existingAccounts = this._accountService.GetAllDTO();

        return existingAccounts;
    }

    [HttpPost, AllowAnonymous] // POST api/Accounts
    public ActionResult Create([FromBody] AccountCreateBind body)
    {
        var existingAccount = this._accountService.GetAccountByUsername(body.Username);

        if (existingAccount != null)
        {
            return BadRequest(new { message = $"Username:{body.Username} is duplicated" });
        }

        var newAccount = new Account
        {
            Username = body.Username,
            Password = HashHelper.Hash(body.Password),
            FirstName = body.FirstName,
            LastName = body.LastName,
            BirthDate = body.BirthDate,
        };

        newAccount = this._accountService.Create(newAccount);

        return CreatedAtAction(nameof(GetDTOById), new { id = newAccount.Id }, newAccount);
    }


    [HttpPut("{id}")] // PUT api/Accounts/{id}
    public ActionResult Update(string id, [FromBody] AccountUpdateBind body)
    {
        var existingAccount = this._accountService.GetById(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        if (ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value != existingAccount.Id)
        {
            return Forbid();
        }

        existingAccount.FirstName = body.FirstName;
        existingAccount.LastName = body.LastName;
        existingAccount.BirthDate = body.BirthDate;

        this._accountService.Update(id, existingAccount);

        return CreatedAtAction(nameof(GetDTOById), new { id = existingAccount.Id }, existingAccount); ;
    }

    [HttpDelete("{id}")] // DELETE api/Accounts/{id}
    public ActionResult Delete(string id)
    {
        var existingAccount = this._accountService.GetById(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        if (ClaimHelper.GetClaim(User.Identity, ClaimTypes.Role).Value == "member" && ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value != existingAccount.Id)
        {
            return Forbid();
        }

        this._accountService.Remove(id);

        return Ok($"Account Id:{id} deleted");
    }

    [HttpPut("ban/{id}"), Authorize(Roles = "admin")] // PUT api/Accounts/ban/{id}
    public ActionResult Ban(string id)
    {
        var existingAccount = this._accountService.GetById(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        existingAccount.IsBanned = true;

        this._accountService.Update(id, existingAccount);

        return NoContent();
    }

    [HttpPut("unban/{id}"), Authorize(Roles = "admin")] // PUT api/Accounts/unban/{id}
    public ActionResult Unban(string id)
    {
        var existingAccount = this._accountService.GetById(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        existingAccount.IsBanned = false;

        this._accountService.Update(id, existingAccount);

        return NoContent();
    }
}