using FitnessPortal.Architecture.Tests.CustomRules;

namespace FitnessPortal.Architecture.Tests.Tests.CoreTests;

[TestClass]
public class CoreTests
{
	private const string EntitiesNamespace = "FitnessPortalAPI.Entities";
	private const string EnumsNamespace = "FitnessPortalAPI.Enums";
	private Assembly _assembly = typeof(FitnessPortalAPI.AssemblyMarker).Assembly;

	[TestMethod]
	public void Entities_ShouldBe_Classes_And_Public()
	{
		// Arrange
		var isEntityTypeRule = new IsEntityTypeRule();

		// Act
		var result = Types.InAssembly(_assembly)
			.That()
			.ResideInNamespace(EntitiesNamespace)
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
			.ResideInNamespace(EnumsNamespace)
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

}
