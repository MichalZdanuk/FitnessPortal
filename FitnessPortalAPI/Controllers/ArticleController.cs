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
        public ActionResult CreateArticle([FromBody]ArticleDto dto)
        {
            var id = _articleService.Create(dto);

            return Created($"/api/article/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArticleDto>> GetAll()
        {
            var result = _articleService.GetAll();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody]UpdateArticleDto dto, [FromRoute]int id)
        {
            _articleService.Update(id, dto);

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            _articleService.RemoveAll();

            return NoContent();
        }

        [HttpDelete("{articleId}")]
        public ActionResult Delete([FromRoute]int articleId) 
        {
            _articleService.Remove(articleId);

            return NoContent();
        }
    }
}
