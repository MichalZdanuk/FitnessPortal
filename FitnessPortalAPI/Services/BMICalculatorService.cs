﻿using FitnessPortalAPI.Entities;
using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Services
{
    public interface IBMICalculatorService
    {
        BMIDto CalculateBMI(CreateBMIQuery dto);
        BMIDto CalculateBMIForAnonymous(CreateBMIQuery dto);
        List<BMIDto> GetAllBMIsForUser();

    }
    public class BMICalculatorService : IBMICalculatorService
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IUserContextService _userContextService;
        private Calculator _calculator = new Calculator();

        public BMICalculatorService(FitnessPortalDbContext context,IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _context = context;
        }
        public BMIDto CalculateBMI(CreateBMIQuery dto)
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

            bmi.UserId = (int)_userContextService.GetUserId;
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

        public List<BMIDto> GetAllBMIsForUser()
        {
            Thread.Sleep(1500);//added to present loading spinner in client app
            var bmis = _context.BMIs.Where(b => b.UserId == _userContextService.GetUserId).ToList();
            var bmisDtos = new List<BMIDto>();

            for (int i = 0; i < bmis.Count; i++)
            {
                bmisDtos.Add(new BMIDto()
                {
                    Id = bmis[i].Id,
                    Date = bmis[i].Date,
                    BMIScore = bmis[i].BMIScore,
                    BMICategory= bmis[i].BMICategory,
                });
            }

            return bmisDtos;
        }
    }
}
