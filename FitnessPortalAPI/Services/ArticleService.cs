using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models;

namespace FitnessPortalAPI.Services
{
    public interface IArticleService
    {
        int Create(ArticleDto dto);
        List<ArticleDto> GetAll();
        ArticleDto GetById(int id);
        void Update(int id, UpdateArticleDto dto);
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
        public ArticleDto GetById(int id)
        {
            var article = _context
                .Articles
                .FirstOrDefault(a => a.Id == id);

            if (article is null)
                throw new NotFoundException("Article not found");

            var result = new ArticleDto()
            {
                Title = article.Title,
                ShortDescription = article.ShortDescription,
                Content = article.Content,
                Category = article.Category,
                DateOfPublication = article.DateOfPublication,
            };

            return result;
        }

        public void Update(int id, UpdateArticleDto dto)
        {
            var article = _context
                .Articles
                .FirstOrDefault(a => a.Id == id);

            if(article == null)
                throw new NotFoundException("Article not found");

            if(dto.Title != "" && dto.Title != null)
                article.Title = dto.Title;
            
            if(dto.ShortDescription != "" && dto.ShortDescription != null)
                article.ShortDescription = dto.ShortDescription;
            
            if(dto.Content != "" && dto.Content != null)
                article.Content = dto.Content;
            
            if(dto.Category != "" && dto.Category != null) 
                article.Category = dto.Category;
            
            _context.SaveChanges();
        }
        public void Remove(int articleId)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == articleId);

            if (article is null)
                throw new NotFoundException("Article not found");

            _context.Remove(article);
            _context.SaveChanges();
        }
        public void RemoveAll()
        {
            var articles = _context.Articles.ToList();

            if (articles is null)
                throw new NotFoundException("Article not found");

            _context.RemoveRange(articles);
            _context.SaveChanges();
        }
        
    }
}