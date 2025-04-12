namespace FitnessPortal.Architecture.Tests;
public static class TypeDefinitionExtensions
{
	public static bool IsPublicClass(this TypeDefinition typeDefinition)
	{
		return typeDefinition.IsPublic && typeDefinition.IsClass;
	}

	public static bool IsPublicInterface(this TypeDefinition typeDefinition)
	{
		return typeDefinition.IsPublic && typeDefinition.IsInterface;
	}

	public static bool HasValidSuffix(this TypeDefinition typeDefinition, string suffix)
	{
		return typeDefinition.Name.EndsWith(suffix, StringComparison.Ordinal);
	}
}
