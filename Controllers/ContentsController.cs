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

    [HttpGet] // GET api/Contents
    public ActionResult<List<Content>> Gets()
    {
        var contents = this._contentService.Gets();
        return contents;
    }

    [HttpPost] // POST api/Contents
    public ActionResult<Content> Create([FromBody] Content content)
    {
        this._contentService.Create(content);

        return CreatedAtAction(nameof(Get), new { id = content.Id }, content);
    }

    [HttpPut("{id}")] // PUT api/Contents/{id}
    public ActionResult<Account> Update(string id, [FromBody] Content content)
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

    [HttpPut("like/{id}")] // PUT api/Contents/like/{id}
    public ActionResult<Account> like(string id)
    {
        /* implement like logic here */
        return NoContent();
    }

    [HttpPut("unlike/{id}")] // PUT api/Contents/unlike/{id}
    public ActionResult<Account> unlike(string id)
    {
        /* implement unlike logic here */
        return NoContent();
    }
}