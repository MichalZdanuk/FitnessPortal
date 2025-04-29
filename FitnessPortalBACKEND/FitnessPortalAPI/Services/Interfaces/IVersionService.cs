using FitnessPortalAPI.Models.Version;

namespace FitnessPortalAPI.Services.Interfaces;

public interface IVersionService
{
	public Task<VersionDto> GetApiVersionAsync();
}
