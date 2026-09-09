const fs = require('fs');

const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
let content = fs.readFileSync(jsPath, 'utf8');

const replacements = [
  {
    from: '"../../assets/img/logo.png"',
    to: '"assets/img/logo.png"'
  },
  {
    from: '"../../assets/img/burger_menu.png"',
    to: '"assets/img/burger_menu.png"'
  },
  {
    from: '["href","/inicio"]',
    to: '["href","inicio/index.html"]'
  },
  {
    from: '["aria-current","page","href","/inicio",1,"nav-link"]',
    to: '["aria-current","page","href","inicio/index.html",1,"nav-link"]'
  },
  {
    from: '["href","/renovaciones",1,"nav-link"]',
    to: '["href","renovaciones/index.html",1,"nav-link"]'
  }
];

replacements.forEach(r => {
  const beforeCount = content.split(r.from).length - 1;
  content = content.split(r.from).join(r.to);
  const afterCount = content.split(r.to).length - 1;
  console.log(`Replaced "${r.from}" with "${r.to}". Before count: ${beforeCount}, After count: ${afterCount}`);
});

fs.writeFileSync(jsPath, content, 'utf8');
console.log('Successfully wrote updated main-4L3USHEJ.js');
