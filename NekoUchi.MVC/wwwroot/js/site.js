// Write your Javascript code.

function Subscribe(el)
{
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

function AddToLesson(el)
{
    var id = el.id;

    // Obrada identifikatora
    var splittedId = id.split("+");
    var wordId = "wordId=" + splittedId[0];
    var token = "token=" + splittedId[1];
    var courseId = "courseId=" + splittedId[2];
    var lessonName = "lessonName=" + splittedId[3];

    var url = "/Lesson/AddWordToLesson?" + wordId + "&" + token + "&" + courseId + "&" + lessonName;

    $.ajax({
        type: "GET",
        url: url,
        success: function () {
            alert("Riječ dodana u lekciju.");
        },
        error: function () {
            alert("Došlo je do pogreške.");
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
