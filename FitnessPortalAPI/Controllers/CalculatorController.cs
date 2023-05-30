using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/calculator")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
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
