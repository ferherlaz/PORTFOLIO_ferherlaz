
const range = 40;
const calcValue = (a, b) => (a/b*range-range/2).toFixed(1);
let timeout;
document.addEventListener('mousemove', ({x, y}) => {
if (timeout) {
    window.cancelAnimationFrame(timeout);
}
    
timeout = window.requestAnimationFrame(() => {
    const cards = document.querySelector(".gallery3");
    const images = document.querySelectorAll(".imgcard3");
    const backgrounds = document.querySelectorAll(".bg");
    const yValue = calcValue(y, window.innerHeight);
    const xValue = calcValue(x, window.innerWidth);
    cards.style.transform = `rotateX(${yValue}deg) rotateY(${xValue}deg)`;
    [].forEach.call(images, (image) => {
    image.style.transform = `translateX(${-xValue}px) translateY(${yValue}px)`;
    image.closest(".card3").style.boxShadow  = `${-xValue}px ${yValue}px 20px 0px #69FFF1`;
    });
    [].forEach.call(backgrounds, (background) => {
    background.style.backgroundPosition = `${xValue*.45}px ${-yValue*.45}px`;
    })
    })
}, false);


