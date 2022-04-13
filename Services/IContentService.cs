using Backend.Models;

namespace Backend.Services
{
    public interface IContentService
    {
        Content Get(string id);

        List<Content> Gets();

        Content Create(Content content);

        void Update(string id, Content content);

        void Remove(string id);
    }
}