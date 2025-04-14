namespace FitnessPortal.Architecture.Tests.Tests.InfrastructureTests;
[TestClass]
public class RepositoryTests
{
	[TestMethod]
	public void Repository_ShouldBe_PublicClass_And_Implement_IRepository_And_Have_Repository_Suffix()
	{
		// Arrange
		var isRepositoryRuleType = new IsRepositoryTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.RepositoryNamespace)
			.Should()
			.MeetCustomRule(isRepositoryRuleType)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name}",
			failMessage: Consts.FailMessages.RepositoryRuleFailure);
	}

	[TestMethod]
	public void Repositories_ShouldBe_In_DALRepositoriesNamespace()
	{
		// Arrange
		var isRepositoryRuleType = new IsRepositoryTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isRepositoryRuleType)
			.Should()
			.ResideInNamespace(Consts.Namespaces.RepositoryNamespace)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name} ({t.Namespace})",
			failMessage: Consts.FailMessages.RepositoryNamespaceRuleFailure);
	}
}
