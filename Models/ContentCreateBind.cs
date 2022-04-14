using Microsoft.AspNetCore.Mvc;

namespace Backend.Models
{
    [ModelMetadataType(typeof(Content))]
    public class ContentCreateBind
    {
        public string Title { get; set; }

        public string ContentMarkdown { get; set; }

        public DateTime CreateDate { get; set; }
    }
}