using FitnessPortal.Architecture.Tests.CustomRules;

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
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => t.Name));
			Assert.Fail($"The following types do not meet the validator conventions: {failingTypes}");
		}
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
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => $"{t.Name} ({t.Namespace})"));
			Assert.Fail($"The following validators are not in the {Consts.Namespaces.ValidatorsNamespace} actual namespaces: {failingTypes}");
		}
	}
}
