using Mono.Cecil;
using NetArchTest.Rules;

namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsEnumTypeRule
	: ICustomRule
{
	private const string _enumSuffix = "Enum";

	public bool MeetsRule(TypeDefinition type)
	{
		if (!type.IsEnum)
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
		return type.Name.EndsWith(_enumSuffix, StringComparison.Ordinal);
	}
}
