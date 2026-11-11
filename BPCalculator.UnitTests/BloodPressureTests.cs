using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPCalculator;

namespace BPCalculator.UnitTests
{
    [TestClass]
    public class BloodPressureTests
    {
        // Allows multiple test cases to run using different input values
        [TestMethod]

        // Test data: systolic, diastolic, expected category
        [DataRow(80, 50, BPCategory.Low)]
        [DataRow(100, 70, BPCategory.Ideal)]
        [DataRow(130, 75, BPCategory.PreHigh)]
        [DataRow(110, 85, BPCategory.PreHigh)]
        [DataRow(145, 85, BPCategory.High)]

        // Boundary-value tests: check the category at the points where
        // one blood pressure category ends and another category begins.
        // This ensures the category thresholds are handled correctly.
        [DataRow(89, 59, BPCategory.Low)]
        [DataRow(119, 79, BPCategory.Ideal)]
        [DataRow(120, 80, BPCategory.PreHigh)]
        [DataRow(140, 90, BPCategory.High)]

        // Receives the values from each DataRow
        public void Category_WithGivenReadings_ReturnsExpectedCategory(
            int systolic, int diastolic, BPCategory expectedCategory)
        {
            // Arrange: create a blood pressure object using the test values
            BloodPressure bp = new BloodPressure()
            {
                Systolic = systolic,
                Diastolic = diastolic
            };

            // Act: calculate the blood pressure category
            BPCategory actualCategory = bp.Category;
            // with the expected category supplied by the DataRow
            Assert.AreEqual(expectedCategory, actualCategory);
        }
    }
}
