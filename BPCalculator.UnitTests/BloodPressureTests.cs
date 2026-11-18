using Microsoft.VisualStudio.TestTools.UnitTesting;
using BPCalculator;
// Gives our tests access to the Razor Page and its testing context
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using BPCalculator.Pages;

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

        // Test that the page initialises with default readings
        [TestMethod]
        public void OnGet_InitialisesDefaultBloodPressure()
        {
            var model = CreatePageModel();

            model.OnGet();

            Assert.AreEqual(100, model.BP.Systolic);
            Assert.AreEqual(60, model.BP.Diastolic);
        }

        // Test that a valid reading is accepted without validation errors
        [TestMethod]
        public void OnPost_ValidReading_ReturnsPageWithoutErrors()
        {
            var model = CreatePageModel();

            model.BP = new BloodPressure
            {
                Systolic = 120,
                Diastolic = 80
            };

            var result = model.OnPost();

            Assert.IsInstanceOfType(result, typeof(PageResult));
            Assert.IsTrue(model.ModelState.IsValid);
        }

        // Test that invalid readings produce a validation error
        [TestMethod]

        [DataRow(80, 80)]
        [DataRow(70, 90)]
        public void OnPost_InvalidReading_AddsModelError(
            int systolic, int diastolic)
        {
            var model = CreatePageModel();

            model.BP = new BloodPressure
            {
                Systolic = systolic,
                Diastolic = diastolic
            };

            model.OnPost();

            Assert.IsFalse(model.ModelState.IsValid);
        }

        // Helper method creates a BloodPressureModel with a test page context.
        // This allows us to test the Razor Page's OnGet and OnPost methods
        // without needing to launch the actual web application.
        private BloodPressureModel CreatePageModel()
        {
            var model = new BloodPressureModel();

            model.PageContext = new PageContext(
                new ActionContext(
                    new DefaultHttpContext(),
                    new RouteData(),
                    new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor(),
                    new ModelStateDictionary()
                )
            );

            return model;
        }
    }
}
