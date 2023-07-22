using FitnessPortalAPI.Models.Training;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/training")]
    [ApiController]
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

    }
}
