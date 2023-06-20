using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle([FromBody]CreateArticleDto dto)
        {
            var id = await _articleService.CreateAsync(dto);

            return Created($"/api/article/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetAllPaginated([FromQuery]ArticleQuery query)
        {
            var result = await _articleService.GetAllPaginatedAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> Get([FromRoute]int id)
        {
            var article = await _articleService.GetByIdAsync(id);

            return Ok(article);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody]UpdateArticleDto dto, [FromRoute]int id)
        {
            await _articleService.UpdateAsync(id, dto);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            await _articleService.RemoveAllAsync();

            return NoContent();
        }

        [HttpDelete("{articleId}")]
        public async Task<ActionResult> Delete([FromRoute]int articleId) 
        {
            await _articleService.RemoveAsync(articleId);

            return NoContent();
        }
    }
}
