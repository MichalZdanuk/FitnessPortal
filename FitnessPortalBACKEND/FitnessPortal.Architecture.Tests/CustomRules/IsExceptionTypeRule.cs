namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsExceptionTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicClass())
		{
			return false;
		}

		if (!InheritsFromException(type))
		{
			return false;
		}

		if (!type.HasValidSuffix(Consts.Sufixes.Exception))
		{
			return false;
		}

		return true;
	}

	private bool InheritsFromException(TypeDefinition type)
	{
		return type.BaseType != null && type.BaseType.FullName == Consts.Sufixes.ExceptionFullName;
	}
}
