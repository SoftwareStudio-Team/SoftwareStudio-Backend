namespace Backend.Models
{
    public interface IDatabaseSettings
    {
        string DatabaseName { get; set; }

        string ConnectionURI { get; set; }

        string AccountsCollectionName { get; set; }

        string ContentsCollectionName { get; set; }

        string CommentsCollectionName { get; set; }
    }
}