const fs = require('fs');

const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
const inicioPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\inicio\\index.html';
const renovacionesPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\renovaciones\\index.html';

const jsContent = fs.readFileSync(jsPath, 'utf8');
const inicioContent = fs.readFileSync(inicioPath, 'utf8');
const renovacionesContent = fs.readFileSync(renovacionesPath, 'utf8');

const oldStrings = [
  '"../../assets/img/logo.png"',
  '"../../assets/img/burger_menu.png"',
  '["href","/inicio"]',
  '["aria-current","page","href","/inicio",1,"nav-link"]',
  '["href","/renovaciones",1,"nav-link"]'
];

const newStrings = [
  '"assets/img/logo.png"',
  '"assets/img/burger_menu.png"',
  '["href","inicio/index.html"]',
  '["aria-current","page","href","inicio/index.html",1,"nav-link"]',
  '["href","renovaciones/index.html",1,"nav-link"]'
];

let failed = false;

console.log('=== Verifying JS File ===');
oldStrings.forEach((oldStr, idx) => {
  const newStr = newStrings[idx];
  if (jsContent.includes(oldStr)) {
    console.error(`FAIL: Old string "${oldStr}" is still present!`);
    failed = true;
  } else {
    console.log(`PASS: Old string "${oldStr}" was successfully removed.`);
  }

  if (!jsContent.includes(newStr)) {
    console.error(`FAIL: New string "${newStr}" is missing!`);
    failed = true;
  } else {
    console.log(`PASS: New string "${newStr}" is present.`);
  }
});

console.log('\n=== Verifying HTML Files ===');
const checkScript = 'document.addEventListener(\'click\'';

if (inicioContent.includes(checkScript)) {
  console.error('FAIL: Click listener script is still present in inicio/index.html!');
  failed = true;
} else {
  console.log('PASS: Click listener script was successfully removed from inicio/index.html.');
}

if (renovacionesContent.includes(checkScript)) {
  console.error('FAIL: Click listener script is still present in renovaciones/index.html!');
  failed = true;
} else {
  console.log('PASS: Click listener script was successfully removed from renovaciones/index.html.');
}

if (failed) {
  process.exit(1);
} else {
  console.log('\nALL VERIFICATIONS PASSED SUCCESSFULLY!');
}
