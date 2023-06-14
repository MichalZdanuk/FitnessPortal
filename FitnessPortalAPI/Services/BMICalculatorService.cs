using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Services
{
    public interface IBMICalculatorService
    {
        BMIDto CalculateBMI(CreateBMIQuery dto, int userId);
        BMIDto CalculateBMIForAnonymous(CreateBMIQuery dto);
        PageResult<BMIDto> GetAllBMIsForUserPaginated(BMIQuery query, int userId);

    }
    public class BMICalculatorService : IBMICalculatorService
    {
        private readonly FitnessPortalDbContext _context;
        private Calculator _calculator = new Calculator();

        public BMICalculatorService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public BMIDto CalculateBMI(CreateBMIQuery dto, int userId)
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
            
            var bmi = new BMI()
            {
                Date = DateTime.Now,
                BMIScore = bmiIndex,
                BMICategory = bmiCategory,
                Height = dto.Height,
                Weight = dto.Weight,
            };

            bmi.UserId = userId;
            _context.BMIs.Add(bmi);
            _context.SaveChanges();

            return bmiDto;
        }
        public BMIDto CalculateBMIForAnonymous(CreateBMIQuery dto)
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

            return bmiDto;
        }
        public PageResult<BMIDto> GetAllBMIsForUserPaginated(BMIQuery query, int userId)
        {
            var baseQuery = _context
                .BMIs
                .Where(b => b.UserId == userId);

            var bmis = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList(); 
                
            var totalItemsCount = baseQuery.Count();

            var bmiDtos = new List<BMIDto>();
            for (int i = 0; i < bmis.Count; i++)
            {
                bmiDtos.Add(new BMIDto()
                {
                    Id = bmis[i].Id,
                    Date = bmis[i].Date,
                    BMIScore = bmis[i].BMIScore,
                    BMICategory = bmis[i].BMICategory,
                });
            }

            var result = new PageResult<BMIDto>(bmiDtos, totalItemsCount, query.PageSize, query.PageNumber);


            return result;
        }

    }
}
