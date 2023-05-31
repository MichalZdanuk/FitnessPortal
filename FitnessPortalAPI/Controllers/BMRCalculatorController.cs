using FitnessPortalAPI.Models;
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
        public ActionResult<BMIDto> CalculateBmiForNotLogged([FromQuery] CreateBMRQuery bmrQuery)
        {
            var calcualtedBMR = _calculatorService.CalculateBMRForAnonymous(bmrQuery);

            return Ok(calcualtedBMR);
        }
    }
}
