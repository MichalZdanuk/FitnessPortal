namespace FitnessPortalAPI.Models.Articles
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime DateOfPublication { get; set; }
    }
}
