const http = require('http');

const url = 'http://localhost:4200/web-seguros/assets/img/logo.png';

http.get(url, (res) => {
  console.log(`Status Code: ${res.statusCode}`);
  console.log(`Headers:`, res.headers);
}).on('error', (e) => {
  console.error(`Got error: ${e.message}`);
});
