using FitnessPortalAPI.Models.Articles;

namespace FitnessPortalAPI.Services.Interfaces;
public interface IArticleService
{
	Task<int> CreateAsync(CreateArticleDto dto);
	Task<PageResult<ArticleDto>> GetPaginatedAsync(ArticleQuery query);
	Task<ArticleDto> GetByIdAsync(int id);
	Task UpdateAsync(int articleId, UpdateArticleDto dto);
	Task RemoveAsync(int articleId);
}
