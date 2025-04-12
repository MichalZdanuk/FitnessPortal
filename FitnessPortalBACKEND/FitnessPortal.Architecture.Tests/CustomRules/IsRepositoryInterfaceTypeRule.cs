namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsRepositoryInterfaceTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsInterface)
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
		var typeName = type.Name;
		return typeName.StartsWith(Consts.Sufixes.InterfacePrefix, StringComparison.Ordinal) &&
			typeName.EndsWith(Consts.Sufixes.RepositorySuffix, StringComparison.Ordinal);
	}
}
