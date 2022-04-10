using Microsoft.AspNetCore.Mvc;

using Backend.Models;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        this._accountService = accountService;
    }

    [HttpGet("{id}")] // GET api/Accounts/{id}
    public ActionResult<Account> Get(string id)
    {
        var account = this._accountService.Get(id);

        if (account == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        return account;
    }

    [HttpPost] // POST api/Accounts
    public ActionResult<Account> Post([FromBody] Account account)
    {
        this._accountService.Create(account);

        return CreatedAtAction(nameof(Get), new { id = account.Id }, account);
    }

    [HttpPut("{id}")] // PUT api/Accounts/{id}
    public ActionResult<Account> Put(string id, [FromBody] Account account)
    {
        var existingAccount = this._accountService.Get(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        this._accountService.Update(id, account);

        return NoContent();
    }

    [HttpDelete("{id}")] // DELETE api/Accounts/{id}
    public ActionResult<Account> Delete(string id)
    {
        var existingAccount = this._accountService.Get(id);

        if (existingAccount == null)
        {
            return NotFound(new { message = $"Account Id:{id} is not found" });
        }

        this._accountService.Remove(id);

        return Ok($"Account Id:{id} deleted");
    }
}