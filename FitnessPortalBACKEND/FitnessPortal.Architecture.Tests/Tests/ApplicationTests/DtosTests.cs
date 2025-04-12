using FitnessPortal.Architecture.Tests.CustomRules;
using FitnessPortalAPI.Models;
using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Models.Trainings;

namespace FitnessPortal.Architecture.Tests.Tests.ApplicationTests;
[TestClass]
public class DtosTests
{
	[TestMethod]
	public void Dtos_ShouldBe_Public_Records_With_Suffix()
	{
		// Arrange
		var isDtoTypeRule = new IsDtoTypeRule();
		var isNotGivenTypeRule = new IsNotGivenTypeRule(new[]
		{
			typeof(TrainingQuery),
			typeof(BMIQuery),
			typeof(CreateBMIQuery),
			typeof(CreateBMRQuery),
			typeof(CreateBodyFatQuery),
			typeof(ArticleQuery),
			typeof(PageResult<>),
		});

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isNotGivenTypeRule)
			.And()
			.ResideInNamespaceStartingWith(Consts.Namespaces.ModelsNamespace)
			.Should()
			.MeetCustomRule(isDtoTypeRule)
			.GetResult();

		// Assert
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => t.Name));
			Assert.Fail($"The following types do not meet the Dtos conventions: {failingTypes}");
		}
	}

	[TestMethod]
	public void Dtos_Should_Exist_In_ModelsNamespace()
	{
		// Arrange
		var isDtoTypeRule = new IsDtoTypeRule();

		// Act
		var result = Types.InAssembly(Consts.Assembly)
			.That()
			.MeetCustomRule(isDtoTypeRule)
			.Should()
			.ResideInNamespaceStartingWith(Consts.Namespaces.ModelsNamespace)
			.GetResult();

		// Assert
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(t => $"{t.Name} ({t.Namespace})"));
			Assert.Fail($"The following Dtos are not in the '{Consts.Namespaces.ModelsNamespace}' actual namespaces: {failingTypes}");
		}
	}
}
