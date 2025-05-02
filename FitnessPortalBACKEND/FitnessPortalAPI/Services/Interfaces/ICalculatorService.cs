using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Services.Interfaces;
public interface ICalculatorService
{
	Task<BMIDto> CalculateBMIAsync(CreateBMIQuery dto);
	Task<BMIDto> CalculateBMIForAnonymousAsync(CreateBMIQuery dto);
	Task<PageResult<BMIDto>> GetAllBMIsForUserPaginatedAsync(BMIQuery query);
	Task<BMRDto> CalculateBMRForAnonymousAsync(CreateBMRQuery bmrQuery);
	Task<BodyFatDto> CalculateBodyFatForAnonymousAsync(CreateBodyFatQuery bodyFatQuery);
}
