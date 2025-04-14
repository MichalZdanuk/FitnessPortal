namespace FitnessPortal.Architecture.Tests.Tests.InfrastructureTests;
[TestClass]
public class DbContextTests
{
	[TestMethod]
	public void DbContext_ShouldBe_PublicClass_And_InheritFrom_DbContext_And_Have_DbContext_Suffix()
	{
		// Arrange
		var isDbContextTypeRule = new IsDbContextTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespaceMatching($@"^{Consts.Namespaces.DbContextNamespace}$")
			.Should()
			.MeetCustomRule(isDbContextTypeRule)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name}",
			failMessage: Consts.FailMessages.DbContextRuleFailure);
	}

	[TestMethod]
	public void DbContexts_ShouldBe_In_DALNamespace()
	{
		// Arrange
		var isDbContextTypeRule = new IsDbContextTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isDbContextTypeRule)
			.Should()
			.ResideInNamespace(Consts.Namespaces.DbContextNamespace)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name} ({t.Namespace})",
			failMessage: Consts.FailMessages.DbContextNamespaceRuleFailure);
	}
}
