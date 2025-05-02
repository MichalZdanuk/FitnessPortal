using FitnessPortalAPI.Models.Articles;

namespace FitnessPortalAPI.Services;

public class ArticleService(IAuthenticationContext authenticationContext,
	IArticleRepository articleRepository,
	IMapper mapper)
		: IArticleService
{
	public async Task<int> CreateAsync(CreateArticleDto dto)
	{
		var article = mapper.Map<Article>(dto);
		article.DateOfPublication = DateTime.Now;
		article.CreatedById = authenticationContext.UserId;

		var articleId = await articleRepository.CreateAsync(article);

		return article.Id;
	}

	public async Task<PageResult<ArticleDto>> GetPaginatedAsync(ArticleQuery query)
	{
		var articles = await articleRepository.GetAllAsync(query.PageNumber, query.PageSize);
		var totalItemsCount = await articleRepository.GetTotalCountAsync();

		var articlesDtos = mapper.Map<List<ArticleDto>>(articles);

		var result = new PageResult<ArticleDto>(articlesDtos, totalItemsCount, query.PageSize, query.PageNumber);

		return result;
	}

	public async Task<ArticleDto> GetByIdAsync(int id)
	{
		var article = await articleRepository.GetByIdAsync(id);

		if (article is null)
			throw new NotFoundException("Article not found");

		var result = mapper.Map<ArticleDto>(article);

		return result;
	}

	public async Task UpdateAsync(int articleId, UpdateArticleDto dto)
	{
		var article = await articleRepository.GetByIdAsync(articleId);

		if (article == null)
			throw new NotFoundException("Article not found");

		mapper.Map(dto, article);

		await articleRepository.UpdateAsync(article);
	}
	public async Task RemoveAsync(int articleId)
	{
		var article = await articleRepository.GetByIdAsync(articleId);

		if (article is null)
			throw new NotFoundException("Article not found");

		await articleRepository.DeleteAsync(article.Id);
	}
}
