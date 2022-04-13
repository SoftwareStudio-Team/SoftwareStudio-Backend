using Microsoft.AspNetCore.Mvc;

using Backend.Models;
using Backend.Services;

namespace Backend.Controllers;

[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        this._commentService = commentService;
    }

    [HttpGet("{id}")] // GET api/Comments/{id}
    public ActionResult<Comment> Get(string id)
    {
        var comment = this._commentService.Get(id);

        if (comment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        return comment;
    }

    [HttpGet] // GET api/Comments
    public ActionResult<List<Comment>> Gets()
    {
        var comments = this._commentService.Gets();
        return comments;
    }

    [HttpPost] // POST api/Comments
    public ActionResult<Comment> Create([FromBody] Comment comment)
    {
        this._commentService.Create(comment);

        return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment);
    }

    [HttpPut("{id}")] // PUT api/Comments/{id}
    public ActionResult Update(string id, [FromBody] Comment comment)
    {
        var existingComment = this._commentService.Get(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        this._commentService.Update(id, comment);

        return NoContent();
    }

    [HttpDelete("{id}")] // DELETE api/Comments/{id}
    public ActionResult Delete(string id)
    {
        var existingComment = this._commentService.Get(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        this._commentService.Remove(id);

        return Ok($"Comment Id:{id} deleted");
    }

    [HttpPut("like/{id}")] // PUT api/Comments/like/{id}
    public ActionResult like(string id)
    {
        /* implement like logic here */
        return NoContent();
    }

    [HttpPut("unlike/{id}")] // PUT api/Comment/unlike/{id}
    public ActionResult unlike(string id)
    {
        /* implement unlike logic here */
        return NoContent();
    }

    [HttpPut("hide/{id}")] // PUT api/Comments/hide/{id}
    public ActionResult hide(string id)
    {
        var existingComment = this._commentService.Get(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        existingComment.IsHid = true;
        this._commentService.Update(id, existingComment);

        return NoContent();
    }

    [HttpPut("unhide/{id}")] // PUT api/Comment/unhide/{id}
    public ActionResult unhide(string id)
    {
        var existingComment = this._commentService.Get(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        existingComment.IsHid = false;
        this._commentService.Update(id, existingComment);

        return NoContent();
    }
}