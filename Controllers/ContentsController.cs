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
public class ContentsController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentsController(IContentService contentService)
    {
        this._contentService = contentService;
    }

    [HttpGet("{id}"), AllowAnonymous] // GET api/Contents/{id}
    public ActionResult<ContentDTO> GetDTOById(string id)
    {
        var existingContent = this._contentService.GetDTOById(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        return existingContent;
    }

    [HttpGet, AllowAnonymous] // GET api/Contents
    public ActionResult<List<ContentDTO>> GetAllDTO()
    {
        var existingContents = this._contentService.GetAllDTO();

        return existingContents;
    }

    [HttpPost, Authorize(Roles = "admin")] // POST api/Contents
    public ActionResult<Content> Create([FromBody] ContentCreateBind body)
    {
        var newContent = new Content
        {
            Title = body.Title,
            ContentMarkdown = body.ContentMarkdown,
            CreateDate = DateTime.Now,
        };

        newContent = this._contentService.Create(newContent);

        return CreatedAtAction(nameof(GetDTOById), new { id = newContent.Id }, newContent);
    }

    [HttpPut("{id}"), Authorize(Roles = "admin")] // PUT api/Contents/{id}
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

    [HttpDelete("{id}"), Authorize(Roles = "admin")] // DELETE api/Contents/{id}
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
    public ActionResult Like(string id)
    {
        var existingContent = this._contentService.GetById(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        var likeContentObj = new LikeContent()
        {
            ContentId = id,
            AccountId = ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value
        };

        var isLiked = this._contentService.IsLiked(likeContentObj);
        if (isLiked)
        {
            return BadRequest(new { message = $"Content Id:{likeContentObj.ContentId} has been liked by Account Id:{likeContentObj.AccountId}" });
        }

        this._contentService.Like(likeContentObj);

        return NoContent();
    }

    [HttpPut("unlike/{id}")] // PUT api/Contents/unlike/{id}
    public ActionResult Unlike(string id)
    {
        var existingContent = this._contentService.GetById(id);

        if (existingContent == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        var likeContentObj = new LikeContent()
        {
            ContentId = id,
            AccountId = ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value
        };

        this._contentService.Unlike(likeContentObj);

        return NoContent();
    }
}