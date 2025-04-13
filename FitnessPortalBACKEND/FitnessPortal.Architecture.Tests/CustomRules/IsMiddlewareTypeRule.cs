using Microsoft.AspNetCore.Http;

namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsMiddlewareTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicClass())
		{
			return false;
		}

		if (!ImplementsIMiddleware(type))
		{
			return false;
		}

		if (!type.HasValidSuffix(Consts.Sufixes.MiddlewareSuffix))
		{
			return false;
		}

		return true;
	}

	private bool ImplementsIMiddleware(TypeDefinition type)
	{
		return type.Interfaces
			.Any(i => i.InterfaceType.FullName == Consts.Sufixes.MiddlewareFullName);
	}
}
