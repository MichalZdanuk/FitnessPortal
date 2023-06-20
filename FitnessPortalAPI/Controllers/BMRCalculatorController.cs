using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/calculator/bmr")]
    [ApiController]
    public class BMRCalculatorController : ControllerBase
    {
        private readonly IBMRCalculatorService _calculatorService;

        public BMRCalculatorController(IBMRCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }
        [HttpGet("anonymous")]
        public async Task<ActionResult<BMRDto>> CalculateBmiForNotLogged([FromQuery] CreateBMRQuery bmrQuery)
        {
            var calcualtedBMR = await _calculatorService.CalculateBMRForAnonymous(bmrQuery);

            return Ok(calcualtedBMR);
        }
    }
}
