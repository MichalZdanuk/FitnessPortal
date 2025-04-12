namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsRepositoryInterfaceTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicInterface())
		{
			return false;
		}

		if (!(type.HasValidPrefix(Consts.Sufixes.InterfacePrefix) && type.HasValidSuffix(Consts.Sufixes.RepositorySuffix)))
		{
			return false;
		}

		return true;
	}
}
