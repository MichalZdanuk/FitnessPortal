namespace FitnessPortal.Architecture.Tests.Tests.ApplicationTests;
[TestClass]
public class ValidatorsTests
{
	[TestMethod]
	public void Validators_Should_Inherit_AbstractValidator_And_Have_Validator_Sufix()
	{
		// Arrange
		var isValidatorTypeRule = new IsValidatorTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.ValidatorsNamespace)
			.Should()
			.MeetCustomRule(isValidatorTypeRule)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => t.Name,
			failMessage: Consts.FailMessages.ValidatorRuleFailure);
	}

	[TestMethod]
	public void Validators_ShoulBe_In_Validators_Namespace()
	{
		// Arrange
		var isValidatorTypeRule = new IsValidatorTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isValidatorTypeRule)
			.Should()
			.ResideInNamespace(Consts.Namespaces.ValidatorsNamespace)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name} ({t.Namespace})",
			failMessage: Consts.FailMessages.ValidatorNamespaceRuleFailure);
	}
}
