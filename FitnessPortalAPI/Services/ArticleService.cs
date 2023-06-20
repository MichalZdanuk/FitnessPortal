using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitnessPortalAPI.Services
{
    public interface IArticleService
    {
        Task<int> CreateAsync(CreateArticleDto dto);
        Task<PageResult<ArticleDto>> GetAllPaginatedAsync(ArticleQuery query);
        Task<ArticleDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, UpdateArticleDto dto);
        Task RemoveAllAsync();
        Task RemoveAsync(int articleId);

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
        public async Task<int> CreateAsync(CreateArticleDto dto)
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
            await _context.SaveChangesAsync();

            return article.Id;
        }
        public async Task<PageResult<ArticleDto>> GetAllPaginatedAsync(ArticleQuery query)
        {
            var baseQuery = _context
                .Articles
                .Include(a => a.CreatedBy);

            var articles = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            var totalItemsCount = await baseQuery.CountAsync();

            var articlesDtos = new List<ArticleDto>();
            for (int i = 0; i < articles.Count; i++)
            {
                articlesDtos.Add(new ArticleDto()
                {
                    Id = articles[i].Id,
                    Author = articles[i].CreatedBy.Username,
                    Title = articles[i].Title,
                    ShortDescription = articles[i].ShortDescription,
                    Content = articles[i].Content,
                    Category = articles[i].Category,
                    DateOfPublication = articles[i].DateOfPublication,
                });
            }

            var result = new PageResult<ArticleDto>(articlesDtos, totalItemsCount, query.PageSize, query.PageNumber);


            return result;
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            var article = await _context
                .Articles
                .Include(a => a.CreatedBy)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article is null)
                throw new NotFoundException("Article not found");

            var result = new ArticleDto()
            {
                Id = article.Id,
                Author = article.CreatedBy.Username,
                Title = article.Title,
                ShortDescription = article.ShortDescription,
                Content = article.Content,
                Category = article.Category,
                DateOfPublication = article.DateOfPublication,
            };

            return result;
        }

        public async Task UpdateAsync(int id, UpdateArticleDto dto)
        {
            var article = await _context
                .Articles
                .FirstOrDefaultAsync(a => a.Id == id);

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
        public async Task RemoveAsync(int articleId)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);

            if (article is null)
                throw new NotFoundException("Article not found");

            _context.Remove(article);
            _context.SaveChanges();
        }
        public async Task RemoveAllAsync()
        {
            var articles = await _context.Articles.ToListAsync();

            if (articles is null)
                throw new NotFoundException("Article not found");

            _context.RemoveRange(articles);
            _context.SaveChanges();
        }
        
    }
}