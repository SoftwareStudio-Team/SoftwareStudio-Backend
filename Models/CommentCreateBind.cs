using Microsoft.AspNetCore.Mvc;

namespace Backend.Models
{
    [ModelMetadataType(typeof(Comment))]
    public class CommentCreateBind
    {
        public string CommentMessage { get; set; }

        public string ContentId { get; set; }

        public string OwnerId { get; set; }
    }
}