namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsExceptionTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsClass)
		{
			return false;
		}

		if (!InheritsFromException(type))
		{
			return false;
		}

		if (!HasValidName(type))
		{
			return false;
		}

		return true;
	}

	private bool InheritsFromException(TypeDefinition type)
	{
		return type.BaseType != null && type.BaseType.FullName == Consts.Sufixes.ExceptionFullName;
	}

	private bool HasValidName(TypeDefinition type)
	{
		return type.Name.EndsWith(Consts.Sufixes.Exception, StringComparison.Ordinal);
	}
}
