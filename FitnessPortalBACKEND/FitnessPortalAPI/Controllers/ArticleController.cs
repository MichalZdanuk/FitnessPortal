using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Services.Interfaces;
using FitnessPortalAPI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ArticleController(IArticleService articleService, IHttpContextAccessor contextAccessor)
        {
            _articleService = articleService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle([FromBody] CreateArticleDto dto)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var id = await _articleService.CreateAsync(dto, userId);

            return Created($"/api/article/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticlesPaginated([FromQuery] ArticleQuery query)
        {
            var result = await _articleService.GetPaginatedAsync(query);
            return Ok(result);
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult<ArticleDto>> GetById([FromRoute] int articleId)
        {
            var article = await _articleService.GetByIdAsync(articleId);

            return Ok(article);
        }

        [HttpPut("{articleId}")]
        public async Task<ActionResult> Update([FromBody] UpdateArticleDto dto, [FromRoute] int articleId)
        {
            await _articleService.UpdateAsync(articleId, dto);

            return Ok();
        }

        [HttpDelete("{articleId}")]
        public async Task<ActionResult> Delete([FromRoute] int articleId)
        {
            await _articleService.RemoveAsync(articleId);

            return NoContent();
        }
    }
}
