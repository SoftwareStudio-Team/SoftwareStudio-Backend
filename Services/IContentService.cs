using Backend.Models;

namespace Backend.Services
{
    public interface IContentService
    {
        List<Content> GetAll();

        Content? GetById(string id);

        Content Create(Content content);

        void Update(string id, Content content);

        void Remove(string id);
    }
}