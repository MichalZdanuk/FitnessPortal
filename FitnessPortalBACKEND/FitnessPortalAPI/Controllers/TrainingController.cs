using FitnessPortalAPI.Models.Trainings;
using FitnessPortalAPI.Utilities;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/training")]
    [ApiController]
    [Authorize]
    public class TrainingController(ITrainingService trainingService, IHttpContextAccessor contextAccessor)
        : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> AddTraining([FromBody] CreateTrainingDto dto)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var id = await trainingService.AddTraining(dto, userId);

            return Created($"/api/training/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<TrainingDto>>> GetTrainingsPaginated([FromQuery] TrainingQuery query)
        {
            int userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var trainings = await trainingService.GetTrainingsPaginated(query, userId);

            return Ok(trainings);
        }

        [HttpDelete("{trainingId}")]
        public async Task<ActionResult> DeleteTraining([FromRoute] int trainingId)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            await trainingService.DeleteTraining(trainingId, userId);

            return NoContent();
        }

        [HttpGet("chart-data")]
        public async Task<ActionResult<IEnumerable<TrainingChartDataDto>>> GetTrainingChartData([FromQuery] TrainingPeriod period)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var filteredTrainings = await trainingService.GetTrainingChartData(period, userId);

            return Ok(filteredTrainings);
        }

        [HttpGet("stats")]
        public async Task<ActionResult<TrainingStatsDto>> GetTrainingStats()
        {
            int userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var trainingStats = await trainingService.GetTrainingStats(userId);

            return Ok(trainingStats);
        }

        [HttpGet("favourite")]
        public async Task<ActionResult<FavouriteExercisesDto>> GetFavouriteExercises()
        {
            int userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var favouriteExercises = await trainingService.GetFavouriteExercises(userId);

            return Ok(favouriteExercises);
        }
    }
}
