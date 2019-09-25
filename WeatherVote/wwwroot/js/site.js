
let qqq
function showa(t) {

    qqq = $(".weather-body", t)

    if (qqq.css('display') == 'none') {
        qqq.css("display", "block");
    }
    else {
        qqq.css("display", "none");

    }
}
