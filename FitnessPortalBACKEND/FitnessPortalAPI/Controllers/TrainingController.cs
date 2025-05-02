using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/training")]
    [ApiController]
    [Authorize]
    public class TrainingController(ITrainingService trainingService)
        : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddTraining([FromBody] CreateTrainingDto dto)
        {
            var id = await trainingService.AddTrainingAsync(dto);

            return Created($"/api/training/{id}", null);
        }

        [HttpGet]
        public async Task<ActionResult<PageResult<TrainingDto>>> GetTrainingsPaginated([FromQuery] TrainingQuery query)
        {
            var trainings = await trainingService.GetTrainingsPaginatedAsync(query);

            return Ok(trainings);
        }

        [HttpDelete("{trainingId}")]
        public async Task<ActionResult> DeleteTraining([FromRoute] int trainingId)
        {
            await trainingService.DeleteTrainingAsync(trainingId);

            return NoContent();
        }

        [HttpGet("chart-data")]
        public async Task<ActionResult<IEnumerable<TrainingChartDataDto>>> GetTrainingChartData([FromQuery] TrainingPeriod period)
        {
            var filteredTrainings = await trainingService.GetTrainingChartDataAsync(period);

            return Ok(filteredTrainings);
        }

        [HttpGet("stats")]
        public async Task<ActionResult<TrainingStatsDto>> GetTrainingStats()
        {
            var trainingStats = await trainingService.GetTrainingStatsAsync();

            return Ok(trainingStats);
        }

        [HttpGet("favourite")]
        public async Task<ActionResult<FavouriteExercisesDto>> GetFavouriteExercises()
        {
            var favouriteExercises = await trainingService.GetFavouriteExercisesAsync();

            return Ok(favouriteExercises);
        }
    }
}
