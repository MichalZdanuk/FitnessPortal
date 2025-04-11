using FitnessPortal.Architecture.Tests.CustomRules;

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
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => t.Name));
			Assert.Fail($"The following types do not meet the Repository interface conventions: {failingTypes}");
		}
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
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => $"{t.Name} ({t.Namespace})"));
			Assert.Fail($"The following Repository interfaces are not in the '{Consts.Namespaces.RepositoryInterfacesNamespace}' actual namespaces: {failingTypes}");
		}
	}
}
