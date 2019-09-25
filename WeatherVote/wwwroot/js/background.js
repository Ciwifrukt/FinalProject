var t = new Date().getHours();

if (t < 12) {
    //document.body.style.backgroundImage = "url('../img/stars.jpg')";
    document.body.style.backgroundImage = "url('../img/sky.jpg')";
} else if (t < 16) {
    document.body.style.backgroundImage = "url('../img/sunset.jpg')";
    //document.body.style.backgroundImage = "url('../img/stars.jpg')";
    //document.body.style.backgroundImage = "url('../img/sky.jpg')";
} else {
    document.body.style.backgroundImage = "url('../img/stars.jpg')";
    //document.body.style.backgroundImage = "url('../img/sky.jpg')";
       }