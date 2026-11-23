
// Import k6 HTTP tools to send GET and POST requests.
import http from 'k6/http';

// Import check to verify responses and sleep to pause between iterations.
import { check, sleep } from 'k6';

// Configure the performance test to simulate up to 50 users.
export const options = {
    stages: [
        // Gradually increase from 0 to 50 users over 1 minute.
        { duration: '1m', target: 50 },

        // Maintain 50 users for 1 minute.
        { duration: '1m', target: 50 },

        // Gradually reduce from 50 users to 0 over 1 minute.
        { duration: '1m', target: 0 },
    ],
};

// The calculator's Azure website address.
const url = 'https://bp-calculator-jomzi-h4hzfwdeehb9b3cn.italynorth-01.azurewebsites.net/';


// The default function runs once for each iteration by each virtual user.
export default function () {

    // Open the calculator page.
    const getResponse = http.get(url);

    // Check that the calculator page loads successfully.
    check(getResponse, {
        'Calculator page loads successfully': (r) => r.status === 200,
    });

    // Find the anti-forgery token in the page's HTML.
    const tokenMatch = getResponse.body.match(
        /name="__RequestVerificationToken"[^>]*value="([^"]+)"/
    );

    // Check that the security token was found.
    check(tokenMatch, {
        'Anti-forgery token is present': (match) => match !== null,
    });

    // Stop this iteration if the security token was not found.
    if (!tokenMatch) {
        sleep(3);
        return;
    }

    // Submit a valid blood pressure reading to the calculator.
    const postResponse = http.post(url, {
        'BP.Systolic': '120',
        'BP.Diastolic': '80',
        '__RequestVerificationToken': tokenMatch[1],
    });

    // Check that the form submission succeeds.
    check(postResponse, {
        'Form submission returns status 200': (r) => r.status === 200,

        // Check that the expected category is displayed.
        'Calculator displays Pre-High Blood Pressure':
            (r) => r.body.includes('Pre-High Blood Pressure'),
    });

    // Pause for 3 seconds before the next iteration.
    sleep(3);
}
