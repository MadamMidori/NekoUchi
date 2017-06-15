// Write your Javascript code.

function Subscribe(el) {
    var id = el.id;    
    var url = "/Course/Subscribe?identification=" + id;
    $.ajax({
        type: "GET",
        url: url,
        success: function () {
            alert("Uspješno ste se pretplatili.");
        },
        error: function () {
            alert("Došlo je do pogreške...");
        }
    });
}

function Unsubscribe(el) {
    var id = el.id;
    var ajaxUrl = "/Course/Unsubscribe?identification=" + id;

    var splittedId = id.split("+");
    var token = splittedId[1];
    var otherUrl = "/Profile/Index?token=" + token;
    $.ajax({
        type: "GET",
        url: ajaxUrl,
        success: function () {
            alert("Uspješno ste se ispisali.");
            localtion.reload(true);
        },
        error: function () {
            alert("Došlo je do pogreške...");
        }
    });
}
