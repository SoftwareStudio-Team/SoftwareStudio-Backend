using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    [BsonIgnoreExtraElements]
    public class LikeComment
    {
        [BsonElement("commentId")]
        public string CommentId { get; set; }

        [BsonElement("accountId")]
        public string AccountId { get; set; }
    }
}