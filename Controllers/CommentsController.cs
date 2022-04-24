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
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IAccountService _accountService;

    public CommentsController(ICommentService commentService, IAccountService accountService)
    {
        this._commentService = commentService;
        this._accountService = accountService;
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
        if (this._accountService.GetDTOById(ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value).IsBanned)
        {
            return Forbid();
        }

        var newComment = new Comment
        {
            CommentMessage = body.CommentMessage,
            ContentId = body.ContentId,
            OwnerId = body.OwnerId,
            CreateDate = DateTime.Now,
        };

        newComment = this._commentService.Create(newComment);

        return CreatedAtAction(nameof(GetDTOById), new { id = newComment.Id }, newComment);
    }

    [HttpPut("{id}")] // PUT api/Comments/{id}
    public ActionResult Update(string id, [FromBody] CommentUpdateBind body)
    {
        if (this._accountService.GetDTOById(ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value).IsBanned)
        {
            return Forbid();
        }

        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        if (ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value != existingComment.OwnerId)
        {
            return Forbid();
        }

        existingComment.CommentMessage = body.CommentMessage;

        this._commentService.Update(id, existingComment);

        return CreatedAtAction(nameof(GetDTOById), new { id = existingComment.Id }, existingComment);
    }

    [HttpDelete("{id}")] // DELETE api/Comments/{id}
    public ActionResult Delete(string id)
    {
        if (this._accountService.GetDTOById(ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value).IsBanned)
        {
            return Forbid();
        }

        var existingComment = this._commentService.GetById(id);

        if (existingComment == null)
        {
            return NotFound(new { message = $"Comment Id:{id} is not found" });
        }

        if (ClaimHelper.GetClaim(User.Identity, ClaimTypes.Role).Value == "member" && ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value != existingComment.OwnerId)
        {
            return Forbid();
        }

        this._commentService.Remove(id);

        return Ok($"Comment Id:{id} deleted");
    }

    [HttpPut("hide/{id}"), Authorize(Roles = "admin")] // PUT api/Comments/hide/{id}
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

    [HttpPut("unhide/{id}"), Authorize(Roles = "admin")] // PUT api/Comment/unhide/{id}
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

        var likeCommentObj = new LikeComment()
        {
            CommentId = id,
            AccountId = ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value
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

        var likeCommentObj = new LikeComment()
        {
            CommentId = id,
            AccountId = ClaimHelper.GetClaim(User.Identity, ClaimTypes.Sid).Value
        };

        this._commentService.Unlike(likeCommentObj);

        return NoContent();
    }
}