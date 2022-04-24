using MongoDB.Driver;

using Backend.DTOs;
using Backend.Models;

namespace Backend.Services
{
    public class ContentService : IContentService
    {
        private readonly IMongoCollection<Content> _contents;
        private readonly IMongoCollection<LikeContent> _likeContents;
        private readonly IMongoCollection<LikeComment> _likeComments;
        private readonly IMongoCollection<Account> _accounts;
        private readonly IMongoCollection<Comment> _comments;

        public ContentService(IDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            this._contents = database.GetCollection<Content>(databaseSettings.ContentsCollectionName);
            this._likeContents = database.GetCollection<LikeContent>(databaseSettings.LikeContentCollectionName);
            this._likeComments = database.GetCollection<LikeComment>(databaseSettings.LikeCommentCollectionName);
            this._accounts = database.GetCollection<Account>(databaseSettings.AccountsCollectionName);
            this._comments = database.GetCollection<Comment>(databaseSettings.CommentsCollectionName);
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

        public List<ContentDTO> GetAllDTO()
        {
            return this._contents.Find(element => true).ToList().Select(element => new ContentDTO()
            {
                Id = element.Id,
                Title = element.Title,
                ContentMarkdown = element.ContentMarkdown,
                CreateDate = element.CreateDate,
                Likes = this.GetAllLike(element.Id),
                Comments = this.GetAllComment(element.Id)
            }).ToList();
        }

        public ContentDTO? GetDTOById(string id)
        {
            try
            {
                var content = this._contents.Find(element => element.Id == id).FirstOrDefault();
                return new ContentDTO()
                {
                    Id = content.Id,
                    Title = content.Title,
                    ContentMarkdown = content.ContentMarkdown,
                    CreateDate = content.CreateDate,
                    Likes = this.GetAllLike(content.Id),
                    Comments = this.GetAllComment(content.Id)
                };
            }
            catch
            {
                return null;
            }
        }

        public List<CommentDTO> GetAllComment(string id)
        {
            try
            {
                return this._comments.Find(element => element.ContentId == id).ToList().Select(element => new CommentDTO()
                {
                    Id = element.Id,
                    CommentMessage = element.CommentMessage,
                    CreateDate = element.CreateDate,
                    IsHid = element.IsHid,
                    ContentId = element.ContentId,
                    Owner = this.GetAccountDetail(element.OwnerId),
                    Likes = this.GetAllCommentLike(element.Id)
                }).ToList();
            }
            catch
            {
                return new List<CommentDTO>();
            }
        }

        public List<AccountDTO> GetAllCommentLike(string commentId)
        {
            try
            {
                return this._likeComments.Find(element => element.CommentId == commentId).ToList().Select(element =>
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

        public List<AccountDTO> GetAllLike(string id)
        {
            try
            {
                return this._likeContents.Find(element => element.ContentId == id).ToList().Select(element =>
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

        public bool IsLiked(LikeContent likeContentObj)
        {
            try
            {
                var obj = this._likeContents.Find(element => element.ContentId == likeContentObj.ContentId && element.AccountId == likeContentObj.AccountId).FirstOrDefault();
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

        public void Like(LikeContent likeContentObj)
        {
            this._likeContents.InsertOne(likeContentObj);
        }

        public void Unlike(LikeContent likeContentObj)
        {
            this._likeContents.DeleteOne(element => element.ContentId == likeContentObj.ContentId && element.AccountId == likeContentObj.AccountId);
        }
    }
}