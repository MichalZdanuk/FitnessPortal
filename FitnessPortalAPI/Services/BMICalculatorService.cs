using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Models.Calculators;
using Microsoft.EntityFrameworkCore;

namespace FitnessPortalAPI.Services
{
    public interface IBMICalculatorService
    {
        Task<BMIDto> CalculateBMI(CreateBMIQuery dto, int userId);
        Task<BMIDto> CalculateBMIForAnonymous(CreateBMIQuery dto);
        Task<PageResult<BMIDto>> GetAllBMIsForUserPaginated(BMIQuery query, int userId);

    }
    public class BMICalculatorService : IBMICalculatorService
    {
        private readonly FitnessPortalDbContext _context;
        private Calculator _calculator = new Calculator();

        public BMICalculatorService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public async Task<BMIDto> CalculateBMI(CreateBMIQuery dto, int userId)
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
            await _context.SaveChangesAsync();

            return bmiDto;
        }
        public async Task<BMIDto> CalculateBMIForAnonymous(CreateBMIQuery dto)
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
        public async Task<PageResult<BMIDto>> GetAllBMIsForUserPaginated(BMIQuery query, int userId)
        {
            Thread.Sleep(1000);//added to present loading spinner in client app

            var baseQuery = _context
                .BMIs
                .Where(b => b.UserId == userId);

            var bmis = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync(); 
                
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
