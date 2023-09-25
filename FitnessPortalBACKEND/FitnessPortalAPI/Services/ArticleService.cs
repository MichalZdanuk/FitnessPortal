using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Exceptions;
using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FitnessPortalAPI.DAL;
using FitnessPortalAPI.Repositories;

namespace FitnessPortalAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
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
                CreatedById = userId
        };

            var articleId = await _articleRepository.CreateAsync(article);

            return article.Id;
        }
        public async Task<PageResult<ArticleDto>> GetAllPaginatedAsync(ArticleQuery query)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app

            var articles = await _articleRepository.GetAllAsync(query.PageNumber, query.PageSize);
            var totalItemsCount = await _articleRepository.GetTotalCountAsync();

            var articlesDtos = articles.Select(article => new ArticleDto
            {
                Id = article.Id,
                Author = article.CreatedBy.Username,
                Title = article.Title,
                ShortDescription = article.ShortDescription,
                Content = article.Content,
                Category = article.Category,
                DateOfPublication = article.DateOfPublication,
            }).ToList();

            var result = new PageResult<ArticleDto>(articlesDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app

            var article = await _articleRepository.GetByIdAsync(id);

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
            var article = await _articleRepository.GetByIdAsync(articleId);

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

            await _articleRepository.UpdateAsync(article);
        }
        public async Task RemoveAsync(int articleId)
        {
            var article = await _articleRepository.GetByIdAsync(articleId);

            if (article is null)
                throw new NotFoundException("Article not found");

            await _articleRepository.DeleteAsync(article.Id);
        }
    }
}
