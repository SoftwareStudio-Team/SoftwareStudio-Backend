using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    [BsonIgnoreExtraElements]
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("commentMessage")]
        public string CommentMessage { get; set; }

        [BsonElement("createDate")]
        public DateTime CreateDate { get; set; }

        [BsonElement("visible")]
        public bool IsVisible { get; set; } = false;

        [BsonElement("contentId")]
        public string ContentId { get; set; }

        [BsonElement("ownerId")]
        public string OwnerId { get; set; }
    }
}