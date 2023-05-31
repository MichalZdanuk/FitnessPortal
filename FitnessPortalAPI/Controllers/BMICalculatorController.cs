using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/calculator/bmi")]
    [ApiController]
    public class BMICalculatorController : ControllerBase
    {
        private readonly IBMICalculatorService _calculatorService;

        public BMICalculatorController(IBMICalculatorService calculatorService)
        {
            _calculatorService= calculatorService;
        }
        [HttpPost]
        public ActionResult<BMIDto> CalculateBmi([FromBody]CreateBMIQuery dto)
        {
        
            var calculatedBMI = _calculatorService.CalculateBMI(dto);

            return Ok(calculatedBMI);
        }

        [HttpGet("anonymous")]
        public ActionResult<BMIDto> CalculateBmiForNotLogged([FromQuery]CreateBMIQuery dto)
        {
            var calcualtedBMI = _calculatorService.CalculateBMIForAnonymous(dto);

            return Ok(calcualtedBMI);
        }

        [HttpGet]
        public ActionResult GetAllBMIs()
        {
            var bmis = _calculatorService.GetAllBMIsForUser();

            return Ok(bmis);
        }
    }
}
