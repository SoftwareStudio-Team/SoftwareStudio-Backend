using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    [BsonIgnoreExtraElements]
    public class LikeContent
    {
        [BsonElement("commentId")]
        public string ContentId { get; set; }

        [BsonElement("accountId")]
        public string AccountId { get; set; }
    }
}