namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsEntityTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsClass)
		{
			return false;
		}

		if (!type.IsPublic)
		{
			return false;
		}

		return true;
	}
}
