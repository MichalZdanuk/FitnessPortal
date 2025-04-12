namespace FitnessPortal.Architecture.Tests.Tests.CoreTests;

[TestClass]
public class CoreTests
{

	[TestMethod]
	public void Entities_ShouldBe_Classes_And_Public()
	{
		// Arrange
		var isEntityTypeRule = new IsEntityTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.EntitiesNamespace)
			.Should()
			.MeetCustomRule(isEntityTypeRule)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => t.Name,
			failMessage: Consts.FailMessages.EntityRuleFailure);
	}

	[TestMethod]
	public void Only_Enums_Should_Exist_In_Enums_Namespace()
	{
		// Arrange
		var isEnumTypeRule = new IsEnumTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.EnumsNamespace)
			.Should()
			.MeetCustomRule(isEnumTypeRule)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => t.Name,
			failMessage: Consts.FailMessages.EnumRuleFailure
			);
	}

	[TestMethod]
	public void Enums_Should_Only_Exist_In_Enums_Namespace()
	{
		// Arrange
		var isEnumTypeRule = new IsEnumTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isEnumTypeRule)
			.Should()
			.ResideInNamespace(Consts.Namespaces.EnumsNamespace)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name} ({t.Namespace})",
			failMessage: Consts.FailMessages.EnumNamespaceRuleFailure);
	}

}
