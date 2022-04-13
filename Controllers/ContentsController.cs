using Microsoft.AspNetCore.Mvc;

using Backend.Models;
using Backend.Services;

namespace Backend.Controllers;

[Route("api/[controller]")]
public class ContentsController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentsController(IContentService contentService)
    {
        this._contentService = contentService;
    }

    [HttpGet("{id}")] // GET api/Contents/{id}
    public ActionResult<Content> Get(string id)
    {
        var content = this._contentService.Get(id);

        if (content == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        return content;
    }

    [HttpPost] // POST api/Contents
    public ActionResult<Content> Post([FromBody] Content content)
    {
        this._contentService.Create(content);

        return CreatedAtAction(nameof(Get), new { id = content.Id }, content);
    }

    [HttpPut("{id}")] // PUT api/Contents/{id}
    public ActionResult<Account> Put(string id, [FromBody] Content content)
    {
        var existingContent = this._contentService.Get(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        this._contentService.Update(id, content);

        return NoContent();
    }

    [HttpDelete("{id}")] // DELETE api/Contents/{id}
    public ActionResult<Account> Delete(string id)
    {
        var existingContent = this._contentService.Get(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        this._contentService.Remove(id);

        return Ok($"Content Id:{id} deleted");
    }
}