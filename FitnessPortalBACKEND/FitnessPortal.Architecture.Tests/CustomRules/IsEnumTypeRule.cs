namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsEnumTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicEnum())
		{
			return false;
		}

		if (!type.HasValidSuffix(Consts.Sufixes.EnumSuffix))
		{
			return false;
		}

		return true;
	}
}
