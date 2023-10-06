using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FitnessPortalAPI.Utilities;
using FitnessPortalAPI.Constants;

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
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var id = await _trainingService.AddTraining(dto, userId);

            return Created($"/api/training/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<TrainingDto>>> GetTrainingsPaginated([FromQuery] TrainingQuery query)
        {
            int userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var trainings = await _trainingService.GetTrainingsPaginated(query, userId);

            return Ok(trainings);
        }

        [HttpDelete("{trainingId}")]
        public async Task<ActionResult> DeleteTraining([FromRoute] int trainingId)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            await _trainingService.DeleteTraining(trainingId, userId);

            return NoContent();
        }

        [HttpGet("chart-data")]
        public async Task<ActionResult<IEnumerable<TrainingChartDataDto>>> GetTrainingChartData([FromQuery] TrainingPeriod period)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var filteredTrainings = await _trainingService.GetTrainingChartData(period, userId);

            return Ok(filteredTrainings);
        }

        [HttpGet("stats")]
        public async Task<ActionResult<TrainingStatsDto>> GetTrainingStats()
        {
            int userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var trainingStats = await _trainingService.GetTrainingStats(userId);

            return Ok(trainingStats);
        }

        [HttpGet("favourite")]
        public async Task<ActionResult<FavouriteExercisesDto>> GetFavouriteExercises()
        {
            int userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var favouriteExercises = await _trainingService.GetFavouriteExercises(userId);

            return Ok(favouriteExercises);
        }
    }
}
