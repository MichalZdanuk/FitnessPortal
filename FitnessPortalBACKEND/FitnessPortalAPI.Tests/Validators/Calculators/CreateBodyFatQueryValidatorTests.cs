using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Models.Calculators;
using FitnessPortalAPI.Validators.Calculators;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPortalAPI.Tests.Validators.Calculators
{
    [TestClass]
    public class CreateBodyFatQueryValidatorTests
    {
        private readonly CreateBodyFatQueryValidator _createBodyFatQueryValidator;
        private CreateBodyFatQuery _defaultCreateBodyFatQuery;
        public CreateBodyFatQueryValidatorTests()
        {
            _createBodyFatQueryValidator = new CreateBodyFatQueryValidator();
            _defaultCreateBodyFatQuery = new CreateBodyFatQuery()
            {
                Sex = Sex.Male,
                Height = 175.2f,
                Waist = 66f,
                Hip = 54f,
                Neck = 37f
            };
        }

        [TestMethod]
        public void CreateBodyFatQueryValidator_ValidData_ShouldBeValid()
        {
            // arrange
            // default case do not need changes

            // act
            var testResult = _createBodyFatQueryValidator.Validate(_defaultCreateBodyFatQuery);

            // assert
            testResult.IsValid.ShouldBeTrue();
            testResult.Errors.Count.ShouldBe(0);
        }

        [TestMethod]
        public void CreateBodyFatQueryValidator_MissingData_ShouldBeInvalid()
        {
            // arrange
            var queryWithMissingData = new CreateBodyFatQuery() { };

            // act
            var testResult = _createBodyFatQueryValidator.Validate(queryWithMissingData);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Height));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Waist));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Hip));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Neck));
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Sex));
        }

        [TestMethod]
        [DataRow(75f)]
        [DataRow(241f)]
        public void CreateBodyFatQueryValidator_HeightValueNotInRange_ShouldBeInvalid(float height)
        {
            // arrange
            _defaultCreateBodyFatQuery.Height = height;

            // act
            var testResult = _createBodyFatQueryValidator.Validate(_defaultCreateBodyFatQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Height));
        }

        [TestMethod]
        [DataRow(29.5f)]
        [DataRow(150.1f)]
        public void CreateBodyFatQueryValidator_WaistValueNotInRange_ShouldBeInvalid(float waist)
        {
            // arrange
            _defaultCreateBodyFatQuery.Waist = waist;

            // act
            var testResult = _createBodyFatQueryValidator.Validate(_defaultCreateBodyFatQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Waist));
        }

        [TestMethod]
        [DataRow(29.5f)]
        [DataRow(101f)]
        public void CreateBodyFatQueryValidator_HipValueNotInRange_ShouldBeInvalid(float hip)
        {
            // arrange
            _defaultCreateBodyFatQuery.Hip = hip;

            // act
            var testResult = _createBodyFatQueryValidator.Validate(_defaultCreateBodyFatQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Hip));
        }

        [TestMethod]
        [DataRow(9.99f)]
        [DataRow(70.1f)]
        public void CreateBodyFatQueryValidator_NeckValueNotInRange_ShouldBeInvalid(float neck)
        {
            // arrange
            _defaultCreateBodyFatQuery.Neck = neck;

            // act
            var testResult = _createBodyFatQueryValidator.Validate(_defaultCreateBodyFatQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Neck));
        }

        [TestMethod]
        [DataRow((Sex)99)]
        public void CreateBodyFatQueryValidator_InvalidSexValue_ShouldBeInvalid(Sex sex)
        {
            // arrange
            _defaultCreateBodyFatQuery.Sex = sex;

            // act
            var testResult = _createBodyFatQueryValidator.Validate(_defaultCreateBodyFatQuery);

            // assert
            testResult.IsValid.ShouldBeFalse();
            testResult.Errors.Count.ShouldBe(1);
            testResult.Errors.ShouldContain(x => x.PropertyName == nameof(CreateBodyFatQuery.Sex));
        }
    }
}
