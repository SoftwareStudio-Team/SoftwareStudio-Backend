using Microsoft.AspNetCore.Mvc;

using Backend.DTOs;
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
    public ActionResult<CommentDTO> GetDTOById(string id)
    {
        var existingComment = this._commentService.GetDTOById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        return existingComment;
    }

    [HttpGet] // GET api/Comments
    public ActionResult<List<CommentDTO>> GetAllDTO()
    {
        var existingComments = this._commentService.GetAllDTO();

        return existingComments;
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

        return CreatedAtAction(nameof(GetDTOById), new { id = newComment.Id }, newComment);
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
        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        /* TODO : change hard code AccountId later */
        var likeCommentObj = new LikeComment()
        {
            CommentId = id,
            AccountId = "625906eecf175fc4739ffd6d"
        };

        var isLiked = this._commentService.IsLiked(likeCommentObj);
        if (isLiked)
        {
            return BadRequest(new { message = $"Comment Id:{likeCommentObj.CommentId} has been liked by Account Id:{likeCommentObj.AccountId}" });
        }

        this._commentService.Like(likeCommentObj);

        return NoContent();
    }

    [HttpPut("unlike/{id}")] // PUT api/Comment/unlike/{id}
    public ActionResult unlike(string id)
    {
        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Content Id:{id} is not found" });
        }

        /* TODO : change hard code AccountId later */
        var likeCommentObj = new LikeComment()
        {
            CommentId = id,
            AccountId = "625906eecf175fc4739ffd6d"
        };

        this._commentService.Unlike(likeCommentObj);

        return NoContent();
    }
}