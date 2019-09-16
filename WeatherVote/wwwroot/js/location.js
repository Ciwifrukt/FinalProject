﻿var locDiv = document.getElementById("locDiv");



function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, showError);
    } else {
        locDiv.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    //locDiv.innerHTML = "Latitude: " + position.coords.latitude +
    //    "<br>Longitude: " + position.coords.longitude;

    let lat = position.coords.latitude
    let lon = position.coords.longitude

    window.location.href = `/Home/GetWeather?lat=${lat}`;
}

function showError(error) {
    switch (error.code) {
        case error.PERMISSION_DENIED:
            locDiv.innerHTML = "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            locDiv.innerHTML = "Location information is unavailable."
            break;
        case error.TIMEOUT:
            locDiv.innerHTML = "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            locDiv.innerHTML = "An unknown error occurred."
            break;
    }
}