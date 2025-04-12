using FitnessPortalAPI.Models.Calculators;

namespace FitnessPortalAPI.Services.Interfaces
{
	public interface ICalculatorService
    {
        Task<BMIDto> CalculateBMI(CreateBMIQuery dto, int userId);
        Task<BMIDto> CalculateBMIForAnonymous(CreateBMIQuery dto);
        Task<PageResult<BMIDto>> GetAllBMIsForUserPaginated(BMIQuery query, int userId);
        Task<BMRDto> CalculateBMRForAnonymous(CreateBMRQuery bmrQuery);
        Task<BodyFatDto> CalculateBodyFatForAnonymous(CreateBodyFatQuery bodyFatQuery);
    }
}
