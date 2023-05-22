using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models;

namespace FitnessPortalAPI.Services
{
    public interface IArticleService
    {
        int Create(ArticleDto dto);
        List<ArticleDto> GetAll();
        void RemoveAll();
        void Remove(int articleId);

    }
    public class ArticleService : IArticleService
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IUserContextService _userContextService;
        public ArticleService(FitnessPortalDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }
        public int Create(ArticleDto dto)
        {
            var article = new Article()
            {
                Title = dto.Title,
                ShortDescription = dto.ShortDescription,
                Content = dto.Content,
                Category = dto.Category,
                DateOfPublication = DateTime.Now,
            };

            article.CreatedById = (int)_userContextService.GetUserId;
            _context.Articles.Add(article);
            _context.SaveChanges();

            return article.Id;
        }

        public List<ArticleDto> GetAll()
        {
            var articles = _context.Articles.ToList();
            var articlesDtos = new List<ArticleDto>();
            for (int i = 0; i < articles.Count; i++)
            {
                articlesDtos.Add(new ArticleDto()
                {
                    Title = articles[i].Title,
                    ShortDescription = articles[i].ShortDescription,
                    Content = articles[i].Content,
                    Category = articles[i].Category,
                    DateOfPublication = articles[i].DateOfPublication,
                });
            }

            return articlesDtos;
        }

        public void Remove(int articleId)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == articleId);
            if (article is null)
            {
                throw new Exception("Bad Request");
            }

            _context.Remove(article);
            _context.SaveChanges();
        }

        public void RemoveAll()
        {
            var articles = _context.Articles.ToList();
            _context.RemoveRange(articles);
            _context.SaveChanges();
        }
    }
}
/*
 public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime DateOfPublication { get; set; }
        public int CreatedById { get; set; }
*/
