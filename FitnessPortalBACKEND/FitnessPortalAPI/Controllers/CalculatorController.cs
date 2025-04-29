using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Utilities;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/calculator")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CalculatorController(ICalculatorService calculatorService, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _calculatorService = calculatorService;
        }

        [HttpPost("bmi")]
        public async Task<ActionResult<BMIDto>> CalculateBmi([FromQuery] CreateBMIQuery dto)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var calculatedBMI = await _calculatorService.CalculateBMIAsync(dto, userId);

            return Ok(calculatedBMI);
        }

        [HttpGet("bmi/anonymous")]
        public async Task<ActionResult<BMIDto>> CalculateBmiForNotLogged([FromQuery] CreateBMIQuery dto)
        {
            var calcualtedBMI = await _calculatorService.CalculateBMIForAnonymousAsync(dto);

            return Ok(calcualtedBMI);
        }

        [HttpGet("bmi")]
        public async Task<ActionResult<IEnumerable<BMIDto>>> GetAllBMIsPaginated([FromQuery] BMIQuery query)
        {
            var userId = HttpContextExtensions.EnsureUserId(_contextAccessor.HttpContext!);

            var bmis = await _calculatorService.GetAllBMIsForUserPaginatedAsync(query, userId);

            return Ok(bmis);
        }

        [HttpGet("bmr/anonymous")]
        public async Task<ActionResult<BMRDto>> CalculateBMRForNotLogged([FromQuery] CreateBMRQuery bmrQuery)
        {
            var calcualtedBMR = await _calculatorService.CalculateBMRForAnonymousAsync(bmrQuery);

            return Ok(calcualtedBMR);
        }

        [HttpGet("bodyFat/anonymous")]
        public async Task<ActionResult<BodyFatDto>> CalculateBodyFatForNotLogged([FromQuery] CreateBodyFatQuery bodyFatQuery)
        {
            var calcualtedBodyFat = await _calculatorService.CalculateBodyFatForAnonymousAsync(bodyFatQuery);

            return Ok(calcualtedBodyFat);
        }
    }
}
