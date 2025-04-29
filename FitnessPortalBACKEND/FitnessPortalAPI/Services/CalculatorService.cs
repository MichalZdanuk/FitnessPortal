using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Services;

public class CalculatorService(ICalculatorRepository calculatorRepository, IMapper mapper)
	: ICalculatorService
{
	private Calculator _calculator = new Calculator();

	public async Task<BMIDto> CalculateBMI(CreateBMIQuery dto, int userId)
	{
		var bmiIndex = 0.0f;
		var bmiCategory = BMICategory.Normalweight;
		_calculator.CalculateBMI(dto.Height, dto.Weight, out bmiIndex, out bmiCategory);

		var bmi = new BMI()
		{
			Date = DateTime.Now,
			BMIScore = bmiIndex,
			BMICategory = bmiCategory,
			Height = dto.Height,
			Weight = dto.Weight,
			UserId = userId,
		};

		await calculatorRepository.AddBmiAsync(bmi);
		var bmiDto = mapper.Map<BMIDto>(bmi);

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
		var bmis = await calculatorRepository.GetBMIsForUserPaginated(userId, query.PageNumber, query.PageSize);
		var totalItemsCount = await calculatorRepository.GetTotalBMIsCountForUser(userId);

		var bmiDtos = mapper.Map<List<BMIDto>>(bmis);

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
