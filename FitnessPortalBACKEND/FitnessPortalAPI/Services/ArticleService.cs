using FitnessPortalAPI.Models.Articles;

namespace FitnessPortalAPI.Services
{
	public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateArticleDto dto, int userId)
        {
            var article = _mapper.Map<Article>(dto);
            article.DateOfPublication = DateTime.Now;
            article.CreatedById = userId;

            var articleId = await _articleRepository.CreateAsync(article);

            return article.Id;
        }

        public async Task<PageResult<ArticleDto>> GetPaginatedAsync(ArticleQuery query)
        {
            var articles = await _articleRepository.GetAllAsync(query.PageNumber, query.PageSize);
            var totalItemsCount = await _articleRepository.GetTotalCountAsync();

            var articlesDtos = _mapper.Map<List<ArticleDto>>(articles);

            var result = new PageResult<ArticleDto>(articlesDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);

            if (article is null)
                throw new NotFoundException("Article not found");

            var result = _mapper.Map<ArticleDto>(article);

            return result;
        }

        public async Task UpdateAsync(int articleId, UpdateArticleDto dto)
        {
            var article = await _articleRepository.GetByIdAsync(articleId);

            if (article == null)
                throw new NotFoundException("Article not found");

            _mapper.Map(dto, article);

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
