var locDiv = document.getElementById("locDiv");



function getLocation() {
    navigator.geolocation.getCurrentPosition(showPosition);
}

function showPosition(position) {

    let lat = position.coords.latitude
    let lon = position.coords.longitude

    window.location.href = `/Weather/GetWeather?lat=${lat}&lon=${lon}`;
}



