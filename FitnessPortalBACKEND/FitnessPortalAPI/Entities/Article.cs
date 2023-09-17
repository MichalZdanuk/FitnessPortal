namespace FitnessPortalAPI.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime DateOfPublication { get; set; }
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
