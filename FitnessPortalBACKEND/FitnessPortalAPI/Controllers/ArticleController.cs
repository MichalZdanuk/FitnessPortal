using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var id = await _articleService.CreateAsync(dto, userId);

            return Created($"/api/article/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAllPaginated([FromQuery] ArticleQuery query)
        {
            var result = await _articleService.GetAllPaginatedAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> Get([FromRoute] int id)
        {
            var article = await _articleService.GetByIdAsync(id);

            return Ok(article);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] UpdateArticleDto dto, [FromRoute] int id)
        {
            await _articleService.UpdateAsync(id, dto);

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
