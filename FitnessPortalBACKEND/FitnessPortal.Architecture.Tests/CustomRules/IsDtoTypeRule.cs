namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsDtoTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicRecord())
		{
			return false;
		}

		if (!type.HasValidSuffix(Consts.Sufixes.DtoSuffix))
		{
			return false;
		}

		return true;
	}
}
