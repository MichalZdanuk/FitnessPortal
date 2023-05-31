using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Services
{
    public interface IBMRCalculatorService
    {
        BMRDto CalculateBMRForAnonymous(CreateBMRQuery bmrQuery);
    }
    public class BMRCalculatorService : IBMRCalculatorService
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IUserContextService _userContextService;
        private Calculator _calculator = new Calculator();
        public BMRCalculatorService(FitnessPortalDbContext context, IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _context = context;
        }
        public BMRDto CalculateBMRForAnonymous(CreateBMRQuery bmrQuery)
        {
            var bmrResult = _calculator.CalculateBMR(bmrQuery.Weight, bmrQuery.Height, bmrQuery.Age, bmrQuery.Sex);
            var bmrDto = new BMRDto()
            {
                BMRScore = bmrResult,
            };

            return bmrDto;
        }
    }
}
