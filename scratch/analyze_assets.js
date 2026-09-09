const fs = require('fs');
const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
const content = fs.readFileSync(jsPath, 'utf8');

const regex = /"\.\.\/\.\.\/assets\/[^"]*"/g;
const matches = content.match(regex);
console.log('Matches for relative asset paths with double dots:', matches);
