namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsEntityTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicClass())
		{
			return false;
		}

		return true;
	}
}
