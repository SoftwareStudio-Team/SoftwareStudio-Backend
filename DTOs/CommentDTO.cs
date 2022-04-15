using Microsoft.AspNetCore.Mvc;

using Backend.Models;

namespace Backend.DTOs
{
    [ModelMetadataType(typeof(Account))]
    public class CommentDTO
    {
        public string Id { get; set; }

        public string CommentMessage { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsHid { get; set; }

        public string ContentId { get; set; }

        public AccountDTO Owner { get; set; }

        public List<AccountDTO> Likes { get; set; }
    }
}

