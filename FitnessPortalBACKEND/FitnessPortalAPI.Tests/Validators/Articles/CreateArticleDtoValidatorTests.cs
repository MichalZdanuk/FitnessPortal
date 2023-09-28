using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Validators.Articles;
using Shouldly;

namespace FitnessPortalAPI.Tests.Validators.Articles
{
    [TestClass]
    public class CreateArticleDtoValidatorTests
    {
        private readonly CreateArticleDtoValidator _createArticleDtoValidator;
        private CreateArticleDto _defaultCreateArticleDto;
        public CreateArticleDtoValidatorTests()
        {
            _createArticleDtoValidator = new CreateArticleDtoValidator();
            _defaultCreateArticleDto = new CreateArticleDto()
            {
                Title= "Title",
                ShortDescription = "ShortDescription",
                Content = "Content Content",
                Category = "Category"
            };
        }

        [TestMethod]
        public void CreateArticleDtoValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _createArticleDtoValidator.Validate(_defaultCreateArticleDto);

            // assert
            testResult.IsValid.ShouldBeTrue();
            testResult.Errors.Count.ShouldBe(0);
        }

        [TestMethod]
        public void CreateArticleDtoValidator_MissingData_ShouldBeInvalid()
        {
            // arrange
            _defaultCreateArticleDto.Title = null;
            _defaultCreateArticleDto.Category = null;

            // act
            var testResult = _createArticleDtoValidator.Validate(_defaultCreateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateArticleDto.Title));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateArticleDto.Category));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long title", true)]
        public void CreateArticleDtoValidator_InvalidlengthTitle_ShouldBeInvalid(string title, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                title += new string('X', 150);
            }
            _defaultCreateArticleDto.Title = title;

            // act
            var testResult = _createArticleDtoValidator.Validate(_defaultCreateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateArticleDto.Title));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long description", true)]
        public void CreateArticleDtoValidator_InvalidlengthShortDescription_ShouldBeInvalid(string shortDescription, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                shortDescription += new string('X', 400);
            }
            _defaultCreateArticleDto.ShortDescription = shortDescription;

            // act
            var testResult = _createArticleDtoValidator.Validate(_defaultCreateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateArticleDto.ShortDescription));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long content", true)]
        public void CreateArticleDtoValidator_InvalidlengthContent_ShouldBeInvalid(string content, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                content += new string('X', 5000);
            }
            _defaultCreateArticleDto.Content = content;

            // act
            var testResult = _createArticleDtoValidator.Validate(_defaultCreateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateArticleDto.Content));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long category", true)]
        public void CreateArticleDtoValidator_InvalidlengthCategory_ShouldBeInvalid(string category, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                category += new string('X', 4000);
            }
            _defaultCreateArticleDto.Category = category;

            // act
            var testResult = _createArticleDtoValidator.Validate(_defaultCreateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateArticleDto.Category));
        }
    }
}
