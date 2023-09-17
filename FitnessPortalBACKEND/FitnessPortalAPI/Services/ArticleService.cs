using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly FitnessPortalDbContext _context;
        public ArticleService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(CreateArticleDto dto, int userId)
        {
            var article = new Article()
            {
                Title = dto.Title,
                ShortDescription = dto.ShortDescription,
                Content = dto.Content,
                Category = dto.Category,
                DateOfPublication = DateTime.Now,
            };

            article.CreatedById = userId;
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return article.Id;
        }
        public async Task<PageResult<ArticleDto>> GetAllPaginatedAsync(ArticleQuery query)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app

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
            Thread.Sleep(1000);//added to present loading spinner in client app

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

        public async Task UpdateAsync(int articleId, UpdateArticleDto dto)
        {
            var article = await _context
                .Articles
                .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                throw new NotFoundException("Article not found");

            if (dto.Title != "" && dto.Title != null)
                article.Title = dto.Title;

            if (dto.ShortDescription != "" && dto.ShortDescription != null)
                article.ShortDescription = dto.ShortDescription;

            if (dto.Content != "" && dto.Content != null)
                article.Content = dto.Content;

            if (dto.Category != "" && dto.Category != null)
                article.Category = dto.Category;

            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(int articleId)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == articleId);

            if (article is null)
                throw new NotFoundException("Article not found");

            _context.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}
