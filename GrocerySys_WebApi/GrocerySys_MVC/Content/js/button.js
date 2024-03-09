var animateButton = function (e) {
    e.preventDefault();

    // Reset animation
    e.target.classList.remove('animate');

    // Trigger reflow
    void e.target.offsetWidth;

    // Add animate class to start the animation
    e.target.classList.add('animate');

    // Remove animate class after the animation duration (700ms)
    setTimeout(function () {
        e.target.classList.remove('animate');
    }, 700);
};

document.addEventListener("DOMContentLoaded", function () {
    var bubblyButtons = document.getElementsByClassName("bubbly-button");

    for (var i = 0; i < bubblyButtons.length; i++) {
        bubblyButtons[i].addEventListener('click', animateButton, false);
    }
});
