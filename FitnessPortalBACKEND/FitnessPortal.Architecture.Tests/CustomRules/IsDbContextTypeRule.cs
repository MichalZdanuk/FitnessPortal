using Microsoft.EntityFrameworkCore;

namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsDbContextTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicClass())
		{
			return false;
		}

		if (!InheritsFromDbContext(type))
		{
			return false;
		}

		if (!type.HasValidSuffix(Consts.Sufixes.DbContext))
		{
			return false;
		}

		return true;
	}

	private bool InheritsFromDbContext(TypeDefinition type)
	{
		return type.BaseType?.FullName == Consts.Sufixes.DbContextFullName;
	}
}
