using FitnessPortalAPI.Models.Articles;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/article")]
    [ApiController]
    public class ArticleController(IArticleService articleService)
        : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateArticle([FromBody] CreateArticleDto dto)
        {
            var id = await articleService.CreateAsync(dto);

            return Created($"/api/article/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticlesPaginated([FromQuery] ArticleQuery query)
        {
            var result = await articleService.GetPaginatedAsync(query);
            return Ok(result);
        }

        [HttpGet("{articleId}")]
        public async Task<ActionResult<ArticleDto>> GetById([FromRoute] int articleId)
        {
            var article = await articleService.GetByIdAsync(articleId);

            return Ok(article);
        }

        [HttpPut("{articleId}")]
        public async Task<ActionResult> Update([FromBody] UpdateArticleDto dto, [FromRoute] int articleId)
        {
            await articleService.UpdateAsync(articleId, dto);

            return Ok();
        }

        [HttpDelete("{articleId}")]
        public async Task<ActionResult> Delete([FromRoute] int articleId)
        {
            await articleService.RemoveAsync(articleId);

            return NoContent();
        }
    }
}
