using Microsoft.AspNetCore.Mvc;

namespace Backend.Models
{
    [ModelMetadataType(typeof(Comment))]
    public class CommentUpdateBind
    {
        public string CommentMessage { get; set; }
    }
}