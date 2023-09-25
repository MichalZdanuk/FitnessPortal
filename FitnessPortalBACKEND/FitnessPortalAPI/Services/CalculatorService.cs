using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FitnessPortalAPI.Constants;
using FitnessPortalAPI.DAL;

namespace FitnessPortalAPI.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly FitnessPortalDbContext _context;
        private Calculator _calculator = new Calculator();

        public CalculatorService(FitnessPortalDbContext context)
        {
            _context = context;
        }
        public async Task<BMIDto> CalculateBMI(CreateBMIQuery dto, int userId)
        {
            var bmiIndex = 0.0f;
            var bmiCategory = BMICategory.Normalweight;
            _calculator.CalculateBMI(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);
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
            var bmiCategory = BMICategory.Normalweight;
            _calculator.CalculateBMI(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);
            var bmiDto = new BMIDto()
            {
                Date = DateTime.Now,
                BMIScore = bmiIndex,
                BMICategory = bmiCategory,
            };

            return await Task.FromResult(bmiDto);
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

        public async Task<BMRDto> CalculateBMRForAnonymous(CreateBMRQuery bmrQuery)
        {
            var bmrResult = _calculator.CalculateBMR(bmrQuery.Height, bmrQuery.Weight, bmrQuery.Age, bmrQuery.Sex);
            var bmrDto = new BMRDto()
            {
                BMRScore = bmrResult,
            };

            return await Task.FromResult(bmrDto);
        }

        public async Task<BodyFatDto> CalculateBodyFatForAnonymous(CreateBodyFatQuery bodyFatQuery)
        {
            var bodyFatResult = _calculator.CalculateBodyFat(bodyFatQuery.Height, bodyFatQuery.Waist, bodyFatQuery.Neck, bodyFatQuery.Hip, bodyFatQuery.Sex);
            var bodyFatDto = new BodyFatDto()
            {
                BodyFatLevel = bodyFatResult,
            };

            return await Task.FromResult(bodyFatDto);
        }
    }
}
