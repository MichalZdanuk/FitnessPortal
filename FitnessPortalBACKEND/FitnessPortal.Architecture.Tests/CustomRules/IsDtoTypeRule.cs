namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsDtoTypeRule
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

		if (!IsRecord(type))
		{
			return false;
		}

		if (!HasValidName(type))
		{
			return false;
		}

		return true;
	}

	private bool HasValidName(TypeDefinition type)
	{
		return type.Name.EndsWith(Consts.Sufixes.DtoSuffix, StringComparison.OrdinalIgnoreCase);
	}

	private bool IsRecord(TypeDefinition type)
	{
		return type.Methods.Any(m => m.Name == "PrintMembers");
	}
}
