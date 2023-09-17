using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models;

namespace FitnessPortalAPI.Services.Interfaces
{
    public interface IArticleService
    {
        Task<int> CreateAsync(CreateArticleDto dto, int userId);
        Task<PageResult<ArticleDto>> GetAllPaginatedAsync(ArticleQuery query);
        Task<ArticleDto> GetByIdAsync(int id);
        Task UpdateAsync(int articleId, UpdateArticleDto dto);
        Task RemoveAsync(int articleId);
    }
}
