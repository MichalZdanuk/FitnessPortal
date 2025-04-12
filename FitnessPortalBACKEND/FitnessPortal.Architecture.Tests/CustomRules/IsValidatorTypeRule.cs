namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsValidatorTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsClass)
		{
			return false;
		}

		if (!InheritsFromAbstractValidator(type))
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
		return type.Name.EndsWith(Consts.Sufixes.ValidatorSuffix, StringComparison.Ordinal);
	}

	private bool InheritsFromAbstractValidator(TypeDefinition type)
	{
		var baseType = type.BaseType as GenericInstanceType;

		return baseType != null && baseType.ElementType.FullName == Consts.Sufixes.AbstractValidatorFullName;
	}
}
