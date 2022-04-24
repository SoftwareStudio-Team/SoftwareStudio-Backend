using MongoDB.Driver;

using Backend.DTOs;
using Backend.Models;

namespace Backend.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMongoCollection<Comment> _comments;
        private readonly IMongoCollection<LikeComment> _likeComments;
        private readonly IMongoCollection<Account> _accounts;

        public CommentService(IDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            this._comments = database.GetCollection<Comment>(databaseSettings.CommentsCollectionName);
            this._likeComments = database.GetCollection<LikeComment>(databaseSettings.LikeCommentCollectionName);
            this._accounts = database.GetCollection<Account>(databaseSettings.AccountsCollectionName);
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

        public List<CommentDTO> GetAllDTO()
        {
            return this._comments.Find(element => true).ToList().Select(element => new CommentDTO()
            {
                Id = element.Id,
                CommentMessage = element.CommentMessage,
                CreateDate = element.CreateDate,
                IsHid = element.IsHid,
                ContentId = element.ContentId,
                Owner = this.GetAccountDetail(element.OwnerId),
                Likes = this.GetAllLike(element.Id)
            }).ToList();
        }

        public CommentDTO? GetDTOById(string id)
        {
            try
            {
                var comment = this._comments.Find(element => element.Id == id).FirstOrDefault();
                return new CommentDTO()
                {
                    Id = comment.Id,
                    CommentMessage = comment.CommentMessage,
                    CreateDate = comment.CreateDate,
                    IsHid = comment.IsHid,
                    ContentId = comment.ContentId,
                    Owner = this.GetAccountDetail(comment.OwnerId),
                    Likes = this.GetAllLike(comment.Id)
                };
            }
            catch
            {
                return null;
            }
        }

        public List<AccountDTO> GetAllLike(string id)
        {
            try
            {
                return this._likeComments.Find(element => element.CommentId == id).ToList().Select(element =>
                {
                    var likedAccount = this._accounts.Find(acc => acc.Id == element.AccountId).FirstOrDefault();
                    return new AccountDTO()
                    {
                        Id = likedAccount.Id,
                        Username = likedAccount.Username,
                        FirstName = likedAccount.FirstName,
                        LastName = likedAccount.LastName,
                    };
                }).ToList();
            }
            catch
            {
                return new List<AccountDTO>();
            }
        }

        public AccountDTO? GetAccountDetail(string accountId)
        {
            try
            {
                var account = this._accounts.Find(element => element.Id == accountId).FirstOrDefault();
                return new AccountDTO()
                {
                    Id = account.Id,
                    Username = account.Username,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Role = account.Role,
                    IsBanned = account.IsBanned
                };
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
            this._likeComments.DeleteMany(element => element.CommentId == id);
        }

        public bool IsLiked(LikeComment likeCommentObj)
        {
            try
            {
                var obj = this._likeComments.Find(element => element.CommentId == likeCommentObj.CommentId && element.AccountId == likeCommentObj.AccountId).FirstOrDefault();
                if (obj != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void Like(LikeComment likeCommentObj)
        {
            this._likeComments.InsertOne(likeCommentObj);
        }

        public void Unlike(LikeComment likeCommentObj)
        {
            this._likeComments.DeleteOne(element => element.CommentId == likeCommentObj.CommentId && element.AccountId == likeCommentObj.AccountId);
        }
    }
}