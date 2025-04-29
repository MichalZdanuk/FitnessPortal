using FitnessPortalAPI.Models.Version;

namespace FitnessPortalAPI.Controllers;

[Route("api/version")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class VersionController(IVersionService versionService)
	: ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<VersionDto>> GetApiVersion()
	{
		var result = await versionService.GetApiVersionAsync();

		return Ok(result);
	}
}
