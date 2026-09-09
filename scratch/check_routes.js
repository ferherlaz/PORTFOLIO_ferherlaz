const fs = require('fs');
const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
const content = fs.readFileSync(jsPath, 'utf8');

const target = 'renovaciones/detalles/:id';
const index = content.indexOf(target);

if (index === -1) {
  console.log('Target not found');
} else {
  console.log('Found routing config:');
  console.log(content.substring(index - 1000, index + 500));
}
