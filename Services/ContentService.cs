using MongoDB.Driver;

using Backend.Models;

namespace Backend.Services
{
    public class ContentService : IContentService
    {
        private readonly IMongoCollection<Content> _contents;
        private readonly IMongoCollection<LikeContent> _likeContents;
        private readonly IMongoCollection<Comment> _comments;

        public ContentService(IDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            this._contents = database.GetCollection<Content>(databaseSettings.ContentsCollectionName);
            this._likeContents = database.GetCollection<LikeContent>(databaseSettings.LikeContentCollectionName);
            this._comments = database.GetCollection<Comment>(databaseSettings.CommentsCollectionName);
        }

        public List<Content> GetAll()
        {
            return this._contents.Find(element => true).ToList();
        }

        public Content? GetById(string id)
        {
            try
            {
                return this._contents.Find(element => element.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public Content Create(Content content)
        {
            this._contents.InsertOne(content);
            return content;
        }

        public void Update(string id, Content content)
        {
            this._contents.ReplaceOne(element => element.Id == id, content);
        }

        public void Remove(string id)
        {
            this._contents.DeleteOne(element => element.Id == id);
            this._likeContents.DeleteMany(element => element.ContentId == id);
            this._comments.DeleteMany(element => element.ContentId == id);
        }
    }
}