using Microsoft.AspNetCore.Mvc;

using Backend.Models;

namespace Backend.DTOs
{
    [ModelMetadataType(typeof(Account))]
    public class ContentDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ContentMarkdown { get; set; }

        public DateTime CreateDate { get; set; }

        public List<AccountDTO> Likes { get; set; }

        public List<CommentDTO> Comments { get; set; }
    }
}

