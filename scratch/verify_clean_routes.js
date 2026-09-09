const fs = require('fs');

const jsPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\main-4L3USHEJ.js';
const inicioPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\inicio\\index.html';
const renovacionesPath = 'C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\renovaciones\\index.html';

const jsContent = fs.readFileSync(jsPath, 'utf8');
const inicioContent = fs.readFileSync(inicioPath, 'utf8');
const renovacionesContent = fs.readFileSync(renovacionesPath, 'utf8');

const expectedJsStrings = [
  '"/web-seguros/assets/img/logo.png"',
  '"/web-seguros/assets/img/burger_menu.png"',
  '["href","inicio"]',
  '["aria-current","page","href","inicio",1,"nav-link"]',
  '["href","renovaciones",1,"nav-link"]'
];

let failed = false;

console.log('=== Verifying JS File ===');
expectedJsStrings.forEach(expectedStr => {
  if (!jsContent.includes(expectedStr)) {
    console.error(`FAIL: Missing string in JS: ${expectedStr}`);
    failed = true;
  } else {
    console.log(`PASS: Found expected string in JS: ${expectedStr}`);
  }
});

console.log('\n=== Verifying HTML Files ===');
const expectedHtml = [
  'href="inicio"',
  'href="renovaciones"'
];

console.log('Checking inicio/index.html...');
expectedHtml.forEach(expectedStr => {
  if (!inicioContent.includes(expectedStr)) {
    console.error(`FAIL: Missing string in inicio/index.html: ${expectedStr}`);
    failed = true;
  } else {
    console.log(`PASS: Found expected string in inicio/index.html: ${expectedStr}`);
  }
});

console.log('Checking renovaciones/index.html...');
expectedHtml.forEach(expectedStr => {
  if (!renovacionesContent.includes(expectedStr)) {
    console.error(`FAIL: Missing string in renovaciones/index.html: ${expectedStr}`);
    failed = true;
  } else {
    console.log(`PASS: Found expected string in renovaciones/index.html: ${expectedStr}`);
  }
});

if (failed) {
  process.exit(1);
} else {
  console.log('\nALL VERIFICATIONS PASSED SUCCESSFULLY!');
}
