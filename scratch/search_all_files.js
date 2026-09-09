const fs = require('fs');
const path = require('path');

const dir = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros';
const targets = [
  '/inicio',
  '/renovaciones',
  '../../assets/img/logo.png',
  '../../assets/img/burger_menu.png'
];

function walk(currentDir) {
  const files = fs.readdirSync(currentDir);
  for (const file of files) {
    const fullPath = path.join(currentDir, file);
    const stat = fs.statSync(fullPath);
    if (stat.isDirectory()) {
      walk(fullPath);
    } else if (stat.isFile()) {
      const ext = path.extname(file);
      if (['.js', '.html', '.css', '.json'].includes(ext)) {
        const content = fs.readFileSync(fullPath, 'utf8');
        targets.forEach(target => {
          if (content.includes(target)) {
            console.log(`Found target "${target}" in file: ${fullPath}`);
          }
        });
      }
    }
  }
}

walk(dir);
