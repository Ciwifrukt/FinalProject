﻿var t = new Date().getHours();

if (t < 10) {
    document.body.style.backgroundImage = "url('../img/sky.jpg')";
} else if (t < 20) {
    document.body.style.backgroundImage = "url('../img/sunset.jpg')";
} else {
    document.body.style.backgroundImage = "url('../img/stars.jpg')";
}