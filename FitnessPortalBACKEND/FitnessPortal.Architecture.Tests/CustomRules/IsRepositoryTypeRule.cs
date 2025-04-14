namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsRepositoryTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicClass())
		{
			return false;
		}

		if (!ImplementsIRepository(type))
		{
			return false;
		}

		if (!type.HasValidSuffix(Consts.Sufixes.RepositorySuffix))
		{
			return false;
		}

		return true;
	}

	private bool ImplementsIRepository(TypeDefinition type)
	{
		return type.Interfaces.Any(i => i.InterfaceType.Name.EndsWith(Consts.Sufixes.RepositorySuffix)
			&& i.InterfaceType.Name.StartsWith(Consts.Sufixes.InterfacePrefix));
	}
}
