using Backend.Models;

namespace Backend.Services
{
    public interface ICommentService
    {
        Comment Get(string id);

        List<Comment> Gets();

        Comment Create(Comment content);

        void Update(string id, Comment content);

        void Remove(string id);
    }
}