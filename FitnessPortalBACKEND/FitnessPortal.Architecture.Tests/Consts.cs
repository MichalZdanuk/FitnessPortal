using FluentValidation;

namespace FitnessPortal.Architecture.Tests;
public class Consts
{
	private const string APISufix = "FitnessPortalAPI";
	public static Assembly Assembly = typeof(FitnessPortalAPI.AssemblyMarker).Assembly;

	public class Namespaces
	{
		public const string EntitiesNamespace = $"{APISufix}.Entities";
		public const string EnumsNamespace = $"{APISufix}.Enums";
		public const string RepositoryInterfacesNamespace = $"{APISufix}.Repositories";
		public const string ValidatorsNamespace = $"{APISufix}.Validators";
		public const string ModelsNamespace = $"{APISufix}.Models";
		public const string ExceptionsNamespace = $"{APISufix}.Exceptions";
	}

	public class Sufixes
	{
		public const string InterfacePrefix = "I";
		public const string RepositorySuffix = "Repository";

		public const string ValidatorSuffix = "Validator";
		public static readonly string AbstractValidatorFullName = typeof(AbstractValidator<>).FullName!;

		public const string DtoSuffix = "Dto";

		public const string EnumSuffix = "Enum";

		public const string Exception = "Exception";
		public static readonly string ExceptionFullName = typeof(Exception).FullName!;
	}

	public class FailMessages
	{
		public const string EntityRuleFailure = "The following types do not meet the Entities type conventions";
		public const string EnumRuleFailure = "The following types do not meet the Enum naming and type conventions";
		public const string EnumNamespaceRuleFailure = $"The enums are not in the '{Consts.Namespaces.EnumsNamespace}' actual namespaces";

		public const string DtosRuleFailure = "The following types do not meet the Dtos conventions";
		public const string DtosNamespaceRuleFailure = $"The Dtos are not in the '{Consts.Namespaces.ModelsNamespace}' actual namespaces";

		public const string RepositoryInterfaceRuleFailure = "The following types do not meet the Repository interface conventions";
		public const string RepositoryInterfaceNamespaceRuleFailure = $"The following Repository interfaces are not in the '{Consts.Namespaces.RepositoryInterfacesNamespace}' actual namespaces";

		public const string ValidatorRuleFailure = "The following types do not meet the validator conventions";
		public const string ValidatorNamespaceRuleFailure = $"The following validators are not in the {Consts.Namespaces.ValidatorsNamespace} actual namespaces";
	
		public const string ExceptionRuleFailure = "The following types do not meet the exception conventions";
		public const string ExceptionNamespaceRuleFailure = $"The following exceptions are not in the {Consts.Namespaces.ExceptionsNamespace} actual namespaces";
	}
}
