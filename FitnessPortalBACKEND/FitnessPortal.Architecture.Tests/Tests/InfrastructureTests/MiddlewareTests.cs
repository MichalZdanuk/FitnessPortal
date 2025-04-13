namespace FitnessPortal.Architecture.Tests.Tests.InfrastructureTests;
[TestClass]
public class MiddlewareTests
{
	[TestMethod]
	public void Middlewares_Should_Implement_IMiddleware_And_Have_Middleware_Sufix()
	{
		// Arrange
		var isMiddlewareTypeRule = new IsMiddlewareTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.MiddlewareNamespace)
			.Should()
			.MeetCustomRule(isMiddlewareTypeRule)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => t.Name,
			failMessage: Consts.FailMessages.MiddlewareRuleFailure);
	}

	[TestMethod]
	public void Validators_ShoulBe_In_Validators_Namespace()
	{
		// Arrange
		var isMiddlewareTypeRule = new IsMiddlewareTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isMiddlewareTypeRule)
			.Should()
			.ResideInNamespace(Consts.Namespaces.MiddlewareNamespace)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name} ({t.Namespace})",
			failMessage: Consts.FailMessages.MiddlewareNamespaceRuleFailure);
	}
}
