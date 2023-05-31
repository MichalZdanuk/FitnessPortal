using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/calculator/bodyFat")]
    [ApiController]
    public class BodyFatCalculatorController : ControllerBase
    {
        private readonly IBodyFatCalculatorService _calculatorService;

        public BodyFatCalculatorController(IBodyFatCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }
        [HttpGet("anonymous")]
        public ActionResult<BodyFatDto> CalculateBmiForNotLogged([FromQuery] CreateBodyFatQuery bodyFatQuery)
        {
            var calcualtedBodyFat = _calculatorService.CalculateBodyFatForAnonymous(bodyFatQuery);

            return Ok(calcualtedBodyFat);
        }
    }
}
