namespace Backend.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string DatabaseName { get; set; } = String.Empty;

        public string ConnectionURI { get; set; } = String.Empty;

        public string AccountsCollectionName { get; set; } = String.Empty;

        public string ContentsCollectionName { get; set; } = String.Empty;

        public string CommentsCollectionName { get; set; } = String.Empty;

        public string LikeContentCollectionName { get; set; } = String.Empty;

        public string LikeCommentCollectionName { get; set; } = String.Empty;
    }
}