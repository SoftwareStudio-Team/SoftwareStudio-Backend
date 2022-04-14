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
    public ActionResult<Content> GetById(string id)
    {
        var existingContent = this._contentService.GetById(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        return existingContent;
    }

    [HttpGet] // GET api/Contents
    public ActionResult<List<Content>> GetAll()
    {
        var existingContents = this._contentService.GetAll();

        return existingContents;
    }

    [HttpPost] // POST api/Contents
    public ActionResult<Content> Create([FromBody] ContentCreateBind body)
    {
        var newContent = new Content
        {
            Title = body.Title,
            ContentMarkdown = body.ContentMarkdown,
            CreateDate = body.CreateDate,
        };

        newContent = this._contentService.Create(newContent);

        return CreatedAtAction(nameof(GetById), new { id = newContent.Id }, newContent);
    }

    [HttpPut("{id}")] // PUT api/Contents/{id}
    public ActionResult Update(string id, [FromBody] ContentUpdateBind body)
    {
        var existingContent = this._contentService.GetById(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        existingContent.Title = body.Title;
        existingContent.ContentMarkdown = body.ContentMarkdown;

        this._contentService.Update(id, existingContent);

        return NoContent();
    }

    [HttpDelete("{id}")] // DELETE api/Contents/{id}
    public ActionResult<Content> Delete(string id)
    {
        var existingContent = this._contentService.GetById(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        this._contentService.Remove(id);

        return Ok($"Content Id:{id} deleted");
    }

    [HttpPut("like/{id}")] // PUT api/Contents/like/{id}
    public ActionResult like(string id)
    {
        /* TODO : implement like logic here */
        return NoContent();
    }

    [HttpPut("unlike/{id}")] // PUT api/Contents/unlike/{id}
    public ActionResult unlike(string id)
    {
        /* TODO : implement unlike logic here */
        return NoContent();
    }
}