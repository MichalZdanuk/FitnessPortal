using FitnessPortal.Architecture.Tests.CustomRules;

namespace FitnessPortal.Architecture.Tests.Tests.CoreTests;

[TestClass]
public class CoreTests
{
	private Assembly _assembly = typeof(FitnessPortalAPI.AssemblyMarker).Assembly;

	[TestMethod]
	public void Entities_ShouldBe_Classes_And_Public()
	{
		// Arrange
		var isEntityTypeRule = new IsEntityTypeRule();

		// Act
		var result = Types.InAssembly(_assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.EntitiesNamespace)
			.Should()
			.MeetCustomRule(isEntityTypeRule)
			.GetResult();

		// Assert
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => t.Name));
			Assert.Fail($"The following types do not meet the Entities type conventions: {failingTypes}");
		}
	}

	[TestMethod]
	public void Only_Enums_Should_Exist_In_Enums_Namespace()
	{
		// Arrange
		var isEnumTypeRule = new IsEnumTypeRule();

		// Act
		var result = Types.InAssembly(_assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.EnumsNamespace)
			.Should()
			.MeetCustomRule(isEnumTypeRule)
			.GetResult();

		// Assert
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(_t => _t.Name));
			Assert.Fail($"The following types do not meet the Enum naming and type conventions: {failingTypes}");
		}
	}

	[TestMethod]
	public void Enums_Should_Only_Exist_In_Enums_Namespace()
	{
		// Arrange
		var isEnumTypeRule = new IsEnumTypeRule();

		// Act
		var result = Types.InAssembly(_assembly)
			.That()
			.MeetCustomRule(isEnumTypeRule)
			.Should()
			.ResideInNamespace(Consts.Namespaces.EnumsNamespace)
			.GetResult();

		// Assert
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => $"{t.Name} ({t.Namespace})"));
			Assert.Fail($"The following Enums are not in '{Consts.Namespaces.EnumsNamespace}' actual namespaces: {failingTypes}");
		}
	}

}
