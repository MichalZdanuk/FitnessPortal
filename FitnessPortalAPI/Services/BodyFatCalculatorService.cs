using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Services
{
    public interface IBodyFatCalculatorService
    {
        Task<BodyFatDto> CalculateBodyFatForAnonymous(CreateBodyFatQuery bodyFatQuery);
    }
    public class BodyFatCalculatorService : IBodyFatCalculatorService
    {
        private readonly FitnessPortalDbContext _context;
        private Calculator _calculator = new Calculator();
        public BodyFatCalculatorService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public async Task<BodyFatDto> CalculateBodyFatForAnonymous(CreateBodyFatQuery bodyFatQuery)
        {
            var bodyFatResult = _calculator.CalculateBodyFat(bodyFatQuery.Height, bodyFatQuery.Waist, bodyFatQuery.Neck, bodyFatQuery.Hip, bodyFatQuery.Sex);
            var bodyFatDto = new BodyFatDto()
            {
                BodyFatLevel = bodyFatResult,
            };

            return bodyFatDto;
        }
    }
}
