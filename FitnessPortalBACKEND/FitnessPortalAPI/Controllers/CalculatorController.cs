using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Controllers
{
	[Route("api/calculator")]
    [ApiController]
    public class CalculatorController(ICalculatorService calculatorService)
        : ControllerBase
    {
        [HttpPost("bmi")]
        public async Task<ActionResult<BMIDto>> CalculateBmi([FromQuery] CreateBMIQuery dto)
        {
            var calculatedBMI = await calculatorService.CalculateBMIAsync(dto);

            return Ok(calculatedBMI);
        }

        [HttpGet("bmi/anonymous")]
        public async Task<ActionResult<BMIDto>> CalculateBmiForNotLogged([FromQuery] CreateBMIQuery dto)
        {
            var calcualtedBMI = await calculatorService.CalculateBMIForAnonymousAsync(dto);

            return Ok(calcualtedBMI);
        }

        [HttpGet("bmi")]
        public async Task<ActionResult<IEnumerable<BMIDto>>> GetAllBMIsPaginated([FromQuery] BMIQuery query)
        {
            var bmis = await calculatorService.GetAllBMIsForUserPaginatedAsync(query);

            return Ok(bmis);
        }

        [HttpGet("bmr/anonymous")]
        public async Task<ActionResult<BMRDto>> CalculateBMRForNotLogged([FromQuery] CreateBMRQuery bmrQuery)
        {
            var calcualtedBMR = await calculatorService.CalculateBMRForAnonymousAsync(bmrQuery);

            return Ok(calcualtedBMR);
        }

        [HttpGet("bodyFat/anonymous")]
        public async Task<ActionResult<BodyFatDto>> CalculateBodyFatForNotLogged([FromQuery] CreateBodyFatQuery bodyFatQuery)
        {
            var calcualtedBodyFat = await calculatorService.CalculateBodyFatForAnonymousAsync(bodyFatQuery);

            return Ok(calcualtedBodyFat);
        }
    }
}
