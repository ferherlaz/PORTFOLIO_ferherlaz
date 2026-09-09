const fs = require('fs');

const inicioHtml = fs.readFileSync('C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\inicio\\index.html', 'utf8');
const renovacionesHtml = fs.readFileSync('C:\\GIT-PORTFOLIO\\PORTFOLIO\\PORTFOLIO_ferherlaz\\public\\web-seguros\\renovaciones\\index.html', 'utf8');

function printMatches(filename, content) {
  console.log(`=== Matches in ${filename} ===`);
  const lines = content.split('\n');
  lines.forEach((line, i) => {
    if (line.includes('/inicio') || line.includes('/renovaciones')) {
      console.log(`${i+1}: ${line.trim()}`);
    }
  });
}

printMatches('inicio/index.html', inicioHtml);
printMatches('renovaciones/index.html', renovacionesHtml);
