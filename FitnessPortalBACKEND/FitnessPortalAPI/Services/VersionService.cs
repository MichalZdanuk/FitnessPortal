using FitnessPortalAPI.Models.Version;
using FitnessPortalAPI.Options;
using Microsoft.Extensions.Options;

namespace FitnessPortalAPI.Services;

public class VersionService(IOptions<VersionOptions> versionOptions)
	: IVersionService
{
	private readonly VersionOptions _versionOptions = versionOptions.Value;

	public Task<VersionDto> GetApiVersionAsync()
	{
		return Task.FromResult(new VersionDto(_versionOptions.ApiVersion));
	}
}
