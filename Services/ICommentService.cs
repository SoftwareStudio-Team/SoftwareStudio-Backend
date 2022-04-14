using Backend.Models;

namespace Backend.Services
{
    public interface ICommentService
    {
        List<Comment> GetAllByContentId(string contentId);

        Comment? GetById(string id);

        Comment Create(Comment content);

        void Update(string id, Comment content);

        void Remove(string id);
    }
}