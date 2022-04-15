using Backend.DTOs;
using Backend.Models;

namespace Backend.Services
{
    public interface IContentService
    {
        Content? GetById(string id);

        List<ContentDTO> GetAllDTO();

        ContentDTO? GetDTOById(string id);

        List<CommentDTO> GetAllComment(string id);

        List<AccountDTO> GetAllLike(string id);

        AccountDTO? GetAccountDetail(string accountId);

        Content Create(Content content);

        void Update(string id, Content content);

        void Remove(string id);

        bool IsLiked(LikeContent likeContentObj);

        void Like(LikeContent likeContentObj);

        void Unlike(LikeContent likeContentObj);
    }
}