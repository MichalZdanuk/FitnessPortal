using FitnessPortalAPI.Models.Training;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/training")]
    [ApiController]
    [Authorize]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;
        private readonly IHttpContextAccessor _contextAccessor;
        public TrainingController(ITrainingService trainingService, IHttpContextAccessor contextAccessor)
        {
            _trainingService = trainingService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult> AddTraining([FromBody] CreateTrainingDto dto)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var id = await _trainingService.AddTraining(dto, userId);

            return Created($"/api/training/{id}", null);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDto>>> GetAllTrainings()
        {
            int userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var trainings = await _trainingService.GetAllTrainings(userId);

            return Ok(trainings);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTraining([FromRoute]int id)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _trainingService.DeleteTraining(id, userId);

            return NoContent();
        }
    }
}
