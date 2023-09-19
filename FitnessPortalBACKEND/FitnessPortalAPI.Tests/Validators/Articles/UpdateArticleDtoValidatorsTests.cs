using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Validators.Articles;
using FluentValidation;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPortalAPI.Tests.Validators.Articles
{
    [TestClass]
    public class UpdateArticleDtoValidatorTests
    {
        private readonly UpdateArticleDtoValidator _updateArticleDtoValidator;
        private UpdateArticleDto _defaultUpdateArticleDto;
        public UpdateArticleDtoValidatorTests()
        {
            _updateArticleDtoValidator = new UpdateArticleDtoValidator();
            _defaultUpdateArticleDto = new UpdateArticleDto()
            {
                Title = "Title",
                ShortDescription = "ShortDescription",
                Content = "Content Content",
                Category = "Category"
            };
        }

        [TestMethod]
        public void UpdateArticleDtoValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _updateArticleDtoValidator.Validate(_defaultUpdateArticleDto);

            // assert
            testResult.IsValid.ShouldBeTrue();
            testResult.Errors.Count.ShouldBe(0);
        }

        [TestMethod]
        public void UpdateArticleDtoValidator_MissingData_ShouldBeInvalid()
        {
            // arrange
            _defaultUpdateArticleDto.Title = null;
            _defaultUpdateArticleDto.Category = null;

            // act
            var testResult = _updateArticleDtoValidator.Validate(_defaultUpdateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(UpdateArticleDto.Title));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(UpdateArticleDto.Category));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long title", true)]
        public void UpdateArticleDtoValidator_InvalidlengthTitle_ShouldBeInvalid(string title, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                title += new string('X', 150);
            }
            _defaultUpdateArticleDto.Title = title;

            // act
            var testResult = _updateArticleDtoValidator.Validate(_defaultUpdateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(UpdateArticleDto.Title));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long description", true)]
        public void UpdateArticleDtoValidator_InvalidlengthShortDescription_ShouldBeInvalid(string shortDescription, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                shortDescription += new string('X', 400);
            }
            _defaultUpdateArticleDto.ShortDescription = shortDescription;

            // act
            var testResult = _updateArticleDtoValidator.Validate(_defaultUpdateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(UpdateArticleDto.ShortDescription));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long content", true)]
        public void UpdateArticleDtoValidator_InvalidlengthContent_ShouldBeInvalid(string content, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                content += new string('X', 5000);
            }
            _defaultUpdateArticleDto.Content = content;

            // act
            var testResult = _updateArticleDtoValidator.Validate(_defaultUpdateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(UpdateArticleDto.Content));
        }

        [TestMethod]
        [DataRow("X", false)]
        [DataRow("Too Long category", true)]
        public void UpdateArticleDtoValidator_InvalidlengthCategory_ShouldBeInvalid(string category, bool isTooLong)
        {
            // arrange
            if (isTooLong)
            {
                category += new string('X', 4000);
            }
            _defaultUpdateArticleDto.Category = category;

            // act
            var testResult = _updateArticleDtoValidator.Validate(_defaultUpdateArticleDto);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(UpdateArticleDto.Category));
        }
    }
}
