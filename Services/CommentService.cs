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

        public Comment Get(string id)
        {
            return this._comments.Find(element => element.Id == id).FirstOrDefault();
        }

        public List<Comment> Gets()
        {
            return this._comments.Find(element => true).ToList();
        }

        public Comment Create(Comment comment)
        {
            this._comments.InsertOne(comment);
            return comment;
        }

        public void Update(string id, Comment comment)
        {
            comment.Id = id;
            this._comments.ReplaceOne(element => element.Id == id, comment);
        }

        public void Remove(string id)
        {
            this._comments.DeleteOne(element => element.Id == id);
        }
    }
}