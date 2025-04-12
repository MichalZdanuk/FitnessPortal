using TestResult = NetArchTest.Rules.TestResult;

namespace FitnessPortal.Architecture.Tests;
public static class TestResultExtensions
{
	public static void AssertSuccess(this TestResult result,
		Func<Type, string> formatFailingType,
		string failMessage)
	{
		if (!result.IsSuccessful)
		{
			var failingTypes = string.Join(", ", result.FailingTypes.Select(formatFailingType));
			Assert.Fail($"{failMessage}: {failingTypes}");
		}
	}
}
