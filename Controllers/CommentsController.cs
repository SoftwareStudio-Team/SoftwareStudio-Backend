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
    public ActionResult<Comment> GetById(string id)
    {
        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        return existingComment;
    }

    [HttpPost] // POST api/Comments
    public ActionResult<Comment> Create([FromBody] CommentCreateBind body)
    {
        var newComment = new Comment
        {
            CommentMessage = body.CommentMessage,
            ContentId = body.ContentId,
            OwnerId = body.OwnerId,
        };

        newComment = this._commentService.Create(newComment);

        return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment);
    }

    [HttpPut("{id}")] // PUT api/Comments/{id}
    public ActionResult Update(string id, [FromBody] CommentUpdateBind body)
    {
        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        existingComment.CommentMessage = body.CommentMessage;

        this._commentService.Update(id, existingComment);

        return NoContent();
    }

    [HttpDelete("{id}")] // DELETE api/Comments/{id}
    public ActionResult Delete(string id)
    {
        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        this._commentService.Remove(id);

        return Ok($"Comment Id:{id} deleted");
    }

    [HttpPut("hide/{id}")] // PUT api/Comments/hide/{id}
    public ActionResult hide(string id)
    {
        var existingComment = this._commentService.GetById(id);

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
        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        existingComment.IsHid = false;

        this._commentService.Update(id, existingComment);

        return NoContent();
    }

    [HttpPut("like/{id}")] // PUT api/Comments/like/{id}
    public ActionResult like(string id)
    {
        /* TODO : implement like logic here */
        return NoContent();
    }

    [HttpPut("unlike/{id}")] // PUT api/Comment/unlike/{id}
    public ActionResult unlike(string id)
    {
        /* TODO : implement unlike logic here */
        return NoContent();
    }
}