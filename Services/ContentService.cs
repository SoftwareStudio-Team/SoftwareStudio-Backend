using MongoDB.Driver;

using Backend.Models;

namespace Backend.Services
{
    public class ContentService : IContentService
    {
        private readonly IMongoCollection<Content> _contents;

        public ContentService(IDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            this._contents = database.GetCollection<Content>(databaseSettings.ContentsCollectionName);
        }

        public Content Get(string id)
        {
            return this._contents.Find(element => element.Id == id).FirstOrDefault();
        }

        public Content Create(Content content)
        {
            this._contents.InsertOne(content);
            return content;
        }

        public void Update(string id, Content content)
        {
            content.Id = id;
            this._contents.ReplaceOne(element => element.Id == id, content);
        }

        public void Remove(string id)
        {
            this._contents.DeleteOne(element => element.Id == id);
        }
    }
}