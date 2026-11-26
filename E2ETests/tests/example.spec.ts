import { test, expect } from '@playwright/test';

// Test that the calculator correctly classifies a blood pressure reading.
test('calculates the correct blood pressure category', async ({ page }) => {

    // Open the local Blood Pressure Calculator.
    await page.goto('http://localhost:5000');

    // Enter the systolic and diastolic readings.
    await page.locator('[name="BP.Systolic"]').fill('120');
    await page.locator('[name="BP.Diastolic"]').fill('80');

    // Submit the blood pressure form.
    await page.getByRole('button', { name: 'Submit' }).click();

    // Verify that the correct category is displayed.
    await expect(page.getByText('Pre-High Blood Pressure')).toBeVisible();
});


test('identifies low blood pressure correctly', async ({ page }) => {

    // Open the local Blood Pressure Calculator.
    await page.goto('http://localhost:5000');

    // Enter a low blood pressure reading.
    await page.locator('[name="BP.Systolic"]').fill('85');
    await page.locator('[name="BP.Diastolic"]').fill('55');

    // Submit the blood pressure form.
    await page.getByRole('button', { name: 'Submit' }).click();

    // Verify that the Low Blood Pressure category is displayed.
    await expect(page.getByText('Low Blood Pressure')).toBeVisible();
});
