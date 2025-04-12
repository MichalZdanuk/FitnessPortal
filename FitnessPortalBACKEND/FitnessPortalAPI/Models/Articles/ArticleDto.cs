namespace FitnessPortalAPI.Models.Articles
{
    public record ArticleDto
    {
        public int Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime DateOfPublication { get; set; }
    }
}
