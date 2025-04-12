namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsValidatorTypeRule
	: ICustomRule
{
	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsPublicClass())
		{
			return false;
		}

		if (!InheritsFromAbstractValidator(type))
		{
			return false;
		}

		if (!type.HasValidSuffix(Consts.Sufixes.ValidatorSuffix))
		{
			return false;
		}

		return true;
	}

	private bool InheritsFromAbstractValidator(TypeDefinition type)
	{
		var baseType = type.BaseType as GenericInstanceType;

		return baseType != null && baseType.ElementType.FullName == Consts.Sufixes.AbstractValidatorFullName;
	}
}
