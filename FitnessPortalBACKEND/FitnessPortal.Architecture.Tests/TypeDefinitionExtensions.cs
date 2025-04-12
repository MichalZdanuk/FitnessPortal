namespace FitnessPortal.Architecture.Tests;
public static class TypeDefinitionExtensions
{
	public static bool IsPublicClass(this TypeDefinition typeDefinition)
	{
		return typeDefinition.IsPublic && typeDefinition.IsClass;
	}

	public static bool IsPublicRecord(this TypeDefinition typeDefinition)
	{
		return typeDefinition.IsPublic && typeDefinition.IsClass && typeDefinition.Methods.Any(m => m.Name == "PrintMembers");
	}

	public static bool IsPublicEnum(this TypeDefinition typeDefinition)
	{
		return typeDefinition.IsPublic && typeDefinition.IsEnum;
	}

	public static bool IsPublicInterface(this TypeDefinition typeDefinition)
	{
		return typeDefinition.IsPublic && typeDefinition.IsInterface;
	}

	public static bool HasValidSuffix(this TypeDefinition typeDefinition, string suffix)
	{
		return typeDefinition.Name.EndsWith(suffix, StringComparison.Ordinal);
	}

	public static bool HasValidPrefix(this TypeDefinition typeDefinition, string prefix)
	{
		return typeDefinition.Name.StartsWith(prefix, StringComparison.Ordinal);
	}
}
