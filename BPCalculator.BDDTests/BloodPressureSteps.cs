

using Reqnroll;
using BPCalculator;

namespace BPCalculator.BDDTests
{
    // Contains the step definitions for the blood pressure BDD scenarios.
    [Binding]
    public class BloodPressureSteps
    {
        private BloodPressure _bloodPressure = new BloodPressure();
        private BPCategory _result;

        // Given: Set the systolic reading using the first scenario's wording.
        [Given("I have a systolic blood pressure reading of {int}")]
        public void SetSystolicBloodPressure(int systolic)
        {
            _bloodPressure = new BloodPressure
            {
                Systolic = systolic
            };
        }

        // Given: Set the systolic reading using the second scenario's wording.
        [Given("the user has entered a systolic reading of {int}")]
        public void SetSystolicReading(int systolic)
        {
            _bloodPressure = new BloodPressure
            {
                Systolic = systolic
            };
        }

        // Given: Set the diastolic reading using the first scenario's wording.
        [Given("I have a diastolic blood pressure reading of {int}")]
        public void SetDiastolicBloodPressure(int diastolic)
        {
            _bloodPressure.Diastolic = diastolic;
        }

        // Given: Set the diastolic reading using the second scenario's wording.
        [Given("the user has entered a diastolic reading of {int}")]
        public void SetDiastolicReading(int diastolic)
        {
            _bloodPressure.Diastolic = diastolic;
        }

        // When: Retrieve the blood pressure classification using the first scenario's wording.
        [When("I request my blood pressure classification")]
        public void CalculateBloodPressureClassification()
        {
            _result = _bloodPressure.Category;
        }

        // When: Retrieve the blood pressure category using the second scenario's wording.
        [When("the user calculates their blood pressure category")]
        public void GetBloodPressureCategory()
        {
            _result = _bloodPressure.Category;
        }

        // Then: Check the expected category using the first scenario's wording.
        [Then("my blood pressure category should be {word}")]
        public void CheckBloodPressureCategory(BPCategory expected)
        {
            Assert.AreEqual(expected, _result);
        }

        // Then: Check the expected category using the second scenario's wording.
        [Then("the result should be {word}")]
        public void CheckExpectedBloodPressureCategory(BPCategory expected)
        {
            Assert.AreEqual(expected, _result);
        }
    }
}
