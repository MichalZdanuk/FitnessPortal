namespace FitnessPortal.Architecture.Tests.Tests.ApplicationTests;
[TestClass]
public class ExceptionsTests
{
	[TestMethod]
	public void Exceptions_ShouldBe_Public_Classes_That_Inherit_From_BaseException()
	{
		// Arrange
		var isExceptionTypeRule = new IsExceptionTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.ExceptionsNamespace)
			.Should()
			.MeetCustomRule(isExceptionTypeRule)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => t.Name,
			failMessage: Consts.FailMessages.ExceptionRuleFailure);
	}

	[TestMethod]
	public void Exceptions_Should_Only_Exist_In_ExceptionsNamespace()
	{
		// Arrange
		var isExceptionTypeRule = new IsExceptionTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isExceptionTypeRule)
			.Should()
			.ResideInNamespace(Consts.Namespaces.ExceptionsNamespace)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name} ({t.Namespace})",
			failMessage: Consts.FailMessages.ExceptionNamespaceRuleFailure);
	}
}
