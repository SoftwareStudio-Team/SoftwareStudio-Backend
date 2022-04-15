using Microsoft.AspNetCore.Mvc;

namespace Backend.Models
{
    [ModelMetadataType(typeof(Content))]
    public class ContentUpdateBind
    {
        public string Title { get; set; }

        public string ContentMarkdown { get; set; }
    }
}