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
        public ActionResult CreateArticle([FromBody]CreateArticleDto dto)
        {
            var id = _articleService.Create(dto);

            return Created($"/api/article/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArticleDto>> GetAllPaginated([FromQuery]ArticleQuery query)
        {
            var result = _articleService.GetAllPaginated(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ArticleDto> Get([FromRoute]int id)
        {
            var article = _articleService.GetById(id);

            return Ok(article);
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
