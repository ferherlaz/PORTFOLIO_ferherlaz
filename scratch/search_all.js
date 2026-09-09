const fs = require('fs');
const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
const content = fs.readFileSync(jsPath, 'utf8');

const targets = [
  '"/inicio"',
  '"/renovaciones"',
  '"../../assets/img/logo.png"',
  '"../../assets/img/burger_menu.png"'
];

targets.forEach(target => {
  let count = 0;
  let idx = content.indexOf(target);
  while (idx !== -1) {
    count++;
    idx = content.indexOf(target, idx + 1);
  }
  console.log(`Target: ${target} -> Count: ${count}`);
});
