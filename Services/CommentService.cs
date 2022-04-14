using MongoDB.Driver;

using Backend.Models;

namespace Backend.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentService(IDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            this._comments = database.GetCollection<Comment>(databaseSettings.CommentsCollectionName);
        }

        public List<Comment> GetAllByContentId(string contentId)
        {
            try
            {
                return this._comments.Find(element => element.ContentId == contentId).ToList();
            }
            catch
            {
                return new List<Comment>();
            }
        }

        public Comment? GetById(string id)
        {
            try
            {
                return this._comments.Find(element => element.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public Comment Create(Comment comment)
        {
            this._comments.InsertOne(comment);
            return comment;
        }

        public void Update(string id, Comment comment)
        {
            this._comments.ReplaceOne(element => element.Id == id, comment);
        }

        public void Remove(string id)
        {
            this._comments.DeleteOne(element => element.Id == id);
        }
    }
}