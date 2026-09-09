const fs = require('fs');
const path = require('path');

const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
const content = fs.readFileSync(jsPath, 'utf8');

const target = 'header-root';
const index = content.indexOf(target);

if (index === -1) {
  console.log('Target not found');
} else {
  console.log('Found target at index:', index);
  console.log('--- BEFORE ---');
  console.log(content.substring(Math.max(0, index - 2000), index));
  console.log('--- AFTER ---');
  console.log(content.substring(index, Math.min(content.length, index + 2000)));
}
