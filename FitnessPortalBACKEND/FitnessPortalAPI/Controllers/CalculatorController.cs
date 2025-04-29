using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Utilities;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/calculator")]
    [ApiController]
    public class CalculatorController(ICalculatorService calculatorService, IHttpContextAccessor contextAccessor)
        : ControllerBase
    {
        [HttpPost("bmi")]
        public async Task<ActionResult<BMIDto>> CalculateBmi([FromQuery] CreateBMIQuery dto)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var calculatedBMI = await calculatorService.CalculateBMI(dto, userId);

            return Ok(calculatedBMI);
        }

        [HttpGet("bmi/anonymous")]
        public async Task<ActionResult<BMIDto>> CalculateBmiForNotLogged([FromQuery] CreateBMIQuery dto)
        {
            var calcualtedBMI = await calculatorService.CalculateBMIForAnonymous(dto);

            return Ok(calcualtedBMI);
        }

        [HttpGet("bmi")]
        public async Task<ActionResult<IEnumerable<BMIDto>>> GetAllBMIsPaginated([FromQuery] BMIQuery query)
        {
            var userId = HttpContextExtensions.EnsureUserId(contextAccessor.HttpContext!);

            var bmis = await calculatorService.GetAllBMIsForUserPaginated(query, userId);

            return Ok(bmis);
        }

        [HttpGet("bmr/anonymous")]
        public async Task<ActionResult<BMRDto>> CalculateBMRForNotLogged([FromQuery] CreateBMRQuery bmrQuery)
        {
            var calcualtedBMR = await calculatorService.CalculateBMRForAnonymous(bmrQuery);

            return Ok(calcualtedBMR);
        }

        [HttpGet("bodyFat/anonymous")]
        public async Task<ActionResult<BodyFatDto>> CalculateBodyFatForNotLogged([FromQuery] CreateBodyFatQuery bodyFatQuery)
        {
            var calcualtedBodyFat = await calculatorService.CalculateBodyFatForAnonymous(bodyFatQuery);

            return Ok(calcualtedBodyFat);
        }
    }
}
