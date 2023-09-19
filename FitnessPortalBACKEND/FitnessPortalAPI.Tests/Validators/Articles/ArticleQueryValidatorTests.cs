using FitnessPortalAPI.Models.Articles;
using FitnessPortalAPI.Validators.Articles;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPortalAPI.Tests.Validators.Articles
{
    [TestClass]
    public class ArticleQueryValidatorTests
    {
        private readonly ArticleQueryValidator _articleQueryValidator;
        private ArticleQuery _defaultArticleQuery;
        public ArticleQueryValidatorTests()
        {
            _articleQueryValidator = new ArticleQueryValidator();
            _defaultArticleQuery = new ArticleQuery()
            {
                PageNumber = 1,
                PageSize = 10
            };
        }

        [TestMethod]
        public void ArticleQueryValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _articleQueryValidator.Validate(_defaultArticleQuery);

            // assert
            testResult.IsValid.ShouldBeTrue();
            testResult.Errors.Count.ShouldBe(0);
        }

        [TestMethod]
        [DataRow(-2)]
        [DataRow(0)]
        public void ArticleQueryValidator_ForbiddenPageNumberValue_ShouldBeInvalid(int pageSize)
        {
            // arrange
            _defaultArticleQuery.PageNumber = pageSize;

            // act
            var testResult = _articleQueryValidator.Validate(_defaultArticleQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(20)]
        public void CreateArticleDtoValidator_PageSizeNotInAllowedSizes_ShouldBeInvalid(int pageSize)
        {
            // arrange
            _defaultArticleQuery.PageSize = pageSize;

            // act
            var testResult = _articleQueryValidator.Validate(_defaultArticleQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
        }
    }
}
