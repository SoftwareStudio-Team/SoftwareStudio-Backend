using Microsoft.AspNetCore.Mvc;

using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Utils;

namespace Backend.Controllers;

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

    [HttpPost] // POST api/Accounts
    public ActionResult Create([FromBody] AccountCreateBind body)
    {
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

        existingAccount.FirstName = body.FirstName;
        existingAccount.LastName = body.LastName;
        existingAccount.BirthDate = body.BirthDate;

        this._accountService.Update(id, existingAccount);

        return NoContent();
    }

    [HttpDelete("{id}")] // DELETE api/Accounts/{id}
    public ActionResult Delete(string id)
    {
        var existingAccount = this._accountService.GetById(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        this._accountService.Remove(id);

        return Ok($"Account Id:{id} deleted");
    }

    [HttpPut("ban/{id}")] // PUT api/Accounts/ban/{id}
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

    [HttpPut("unban/{id}")] // PUT api/Accounts/unban/{id}
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