
let qqq
function showa(t) {

    qqq = $(".weather-body", t)

    if (!$(event.target).closest(".btn", t).length) {

        if (qqq.css('display') == 'none') {
            qqq.css("display", "block");
        }

        else {
            qqq.css("display", "none");

        }
    }
}
