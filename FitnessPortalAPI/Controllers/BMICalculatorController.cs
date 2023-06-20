using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessPortalAPI.Controllers
{
    [Route("api/calculator/bmi")]
    [ApiController]
    public class BMICalculatorController : ControllerBase
    {
        private readonly IBMICalculatorService _calculatorService;
        private readonly IHttpContextAccessor _contextAccessor;

        public BMICalculatorController(IBMICalculatorService calculatorService, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor= contextAccessor;
            _calculatorService= calculatorService;
        }
        [HttpPost]
        public async Task<ActionResult<BMIDto>> CalculateBmi([FromBody]CreateBMIQuery dto)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var calculatedBMI = await _calculatorService.CalculateBMI(dto, userId);

            return Ok(calculatedBMI);
        }

        [HttpGet("anonymous")]
        public async Task<ActionResult<BMIDto>> CalculateBmiForNotLogged([FromQuery]CreateBMIQuery dto)
        {
            var calcualtedBMI = await _calculatorService.CalculateBMIForAnonymous(dto);

            return Ok(calcualtedBMI);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BMIDto>>> GetAllBMIsPaginated([FromQuery]BMIQuery query)
        {
            var userId = int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var bmis = await _calculatorService.GetAllBMIsForUserPaginated(query, userId);

            return Ok(bmis);
        }
    }
}
