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
        public ActionResult CalculateBmi([FromBody]CreateBMIDto dto)
        {
        
            var calculatedBMI = _calculatorService.CalculateBMI(dto);

            return Ok(calculatedBMI);
        }

        [HttpGet]
        public ActionResult GetAllBMIs()
        {
            var bmis = _calculatorService.GetAllBMIsForUser();

            return Ok(bmis);
        }
    }
}
