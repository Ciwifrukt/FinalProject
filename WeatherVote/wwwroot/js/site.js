
let qqq
function showa(t) {

    qqq = $(".weather-body", t)

<<<<<<< HEAD
    if (qqq.css('display') == 'none') {
        qqq.css("display", "block");
    }
    else {
        qqq.css("display", "none");
=======
    if (!$(event.target).closest(".btn", t).length) {

        if (qqq.css('display') == 'none') {
            qqq.css("display", "block");
        }

        else {
            qqq.css("display", "none");
>>>>>>> 87cf20b8dfa2f5b7b9935965f681992c8e45345e

        }
    }
}
