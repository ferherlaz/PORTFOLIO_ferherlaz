const fs = require('fs');
const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
const content = fs.readFileSync(jsPath, 'utf8');

const target = '"/inicio"';
let idx = content.indexOf(target);
let count = 0;
while (idx !== -1) {
  count++;
  console.log(`--- Occurrence ${count} ---`);
  console.log(content.substring(Math.max(0, idx - 100), Math.min(content.length, idx + 100)));
  idx = content.indexOf(target, idx + 1);
}
