using Backend.DTOs;
using Backend.Models;

namespace Backend.Services
{
    public interface ICommentService
    {
        Comment? GetById(string id);

        List<CommentDTO> GetAllDTO();

        CommentDTO? GetDTOById(string id);

        List<AccountDTO> GetAllLike(string id);

        AccountDTO? GetAccountDetail(string accountId);

        Comment Create(Comment content);

        void Update(string id, Comment content);

        void Remove(string id);

        bool IsLiked(LikeComment likeCommentObj);

        void Like(LikeComment likeCommentObj);

        void Unlike(LikeComment likeCommentObj);
    }
}