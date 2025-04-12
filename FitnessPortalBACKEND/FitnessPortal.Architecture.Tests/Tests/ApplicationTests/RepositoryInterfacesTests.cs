namespace FitnessPortal.Architecture.Tests.Tests.ApplicationTests;
[TestClass]
public class RepositoryInterfacesTests
{
	[TestMethod]
	public void RepositoryInterfaces_ShouldBe_Interfaces_And_Have_Repository_Suffix()
	{
		// Arrange
		var isRepositoryInterfaceTypeRule = new IsRepositoryInterfaceTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.ResideInNamespace(Consts.Namespaces.RepositoryInterfacesNamespace)
			.Should()
			.MeetCustomRule(isRepositoryInterfaceTypeRule)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => t.Name,
			failMessage: Consts.FailMessages.RepositoryInterfaceRuleFailure
			);
	}

	[TestMethod]
	public void RepositoryInterfaces_Should_Only_Exist_In_Repositories_Namespace()
	{
		// Arrange
		var isRepositoryInterfaceTypeRule = new IsRepositoryInterfaceTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isRepositoryInterfaceTypeRule)
			.Should()
			.ResideInNamespace(Consts.Namespaces.RepositoryInterfacesNamespace)
			.GetResult();

		// Assert
		result.AssertSuccess(
			formatFailingType: t => $"{t.Name} ({t.Namespace})",
			failMessage: Consts.FailMessages.RepositoryInterfaceNamespaceRuleFailure);
	}
}
