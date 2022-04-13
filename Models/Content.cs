using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Models
{
    [BsonIgnoreExtraElements]
    public class Content
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("contentMarkdown")]
        public string ContentMarkdown{ get; set; }

        [BsonElement("createDate")]
        public DateTime CreateDate { get; set; }
    }
}