using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models;

namespace FitnessPortalAPI.Services
{
    public interface ICalculatorService
    {
        BMIDto CalculateBMI(CreateBMIDto dto);
        List<BMIDto> GetAllBMIsForUser();

    }
    public class CalculatorService : ICalculatorService
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IUserContextService _userContextService;
        private Calculator _calculator = new Calculator();

        public CalculatorService(FitnessPortalDbContext context,IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _context = context;
        }
        public BMIDto CalculateBMI(CreateBMIDto dto)
        {
            var bmiIndex = 0.0f;
            var bmiCategory = "";
            _calculator.CalculateBmi(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);
            var bmiDto = new BMIDto()
            {
                Date = DateTime.Now,
                BMIScore = bmiIndex,
                BMICategory = bmiCategory,
            };
            
            if (_userContextService.GetUserId is not null) // to be fixed
            {
                var bmi = new BMI()
                {
                    Date = DateTime.Now,
                    BMIScore = bmiIndex,
                    BMICategory = bmiCategory,
                    Height = dto.Height,
                    Weight = dto.Weight,
                };
                bmi.UserId = (int)_userContextService.GetUserId;
                _context.BMIs.Add(bmi);
                _context.SaveChanges();
            }


            return bmiDto;
        }

        public List<BMIDto> GetAllBMIsForUser()
        {
            var bmis = _context.BMIs.Where(b => b.UserId == _userContextService.GetUserId).ToList();
            var bmisDtos = new List<BMIDto>();

            for (int i = 0; i < bmis.Count; i++)
            {
                bmisDtos.Add(new BMIDto()
                {
                    Date = bmis[i].Date,
                    BMIScore = bmis[i].BMIScore,
                    BMICategory= bmis[i].BMICategory,
                });
            }

            return bmisDtos;
        }
    }
}
