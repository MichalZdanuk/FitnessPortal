namespace FitnessPortal.Architecture.Tests.CustomRules;
public class IsNotGivenTypeRule
	: ICustomRule
{
	private readonly HashSet<Type> _excludedTypes;

	public IsNotGivenTypeRule(Type excludedType)
	{
		_excludedTypes = new HashSet<Type>() { excludedType };
	}

	public IsNotGivenTypeRule(IEnumerable<Type> excludedTypes)
	{
		_excludedTypes = new HashSet<Type>(excludedTypes);
	}

	public bool MeetsRule(TypeDefinition type)
	{
		return !_excludedTypes.Any(excludedType => IsSameType(type, excludedType));
	}

	private bool IsSameType(TypeDefinition typeDefinition, Type type)
	{
		return typeDefinition.FullName == type.FullName;
	}
}
