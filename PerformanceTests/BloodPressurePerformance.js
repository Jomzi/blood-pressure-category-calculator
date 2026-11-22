import http from 'k6/http';
import { sleep, check } from 'k6';

// Configure the performance test using different load stages.
export const options = {
    stages: [        
        { duration: '1m', target: 20 },

        // Stage 2: Maintain 20 virtual users for 1 minute to test the application under steady load.
        { duration: '1m', target: 20 },

        // Stage 3: Gradually reduce the number of virtual users from 20 to 0 over 1 minute.
        { duration: '1m', target: 0 },
    ],
};


// The default function is executed repeatedly by each virtual user during the test.
export default function () {

    // Send an HTTP GET request to the Blood Pressure Calculator website.
    const response = http.get('https://bp-calculator-jomzi-h4hzfwdeehb9b3cn.italynorth-01.azurewebsites.net/');

    // Check that the website responds successfully with HTTP status code 200.
    check(response, {
        'Website responds with status 200': (r) => r.status === 200,
    });

    // Pause for 3 seconds before the virtual user makes another request.
    sleep(3);
}
