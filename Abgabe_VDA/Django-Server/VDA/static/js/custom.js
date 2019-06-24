/// <reference path="base_functions.js" />
function displayEventDetail(data) {
    
    var strContent = "";
    strContent += "<table>";
    for (let key in data) {
        strContent += "    <tr>";
        strContent += "        <td>" + key + "</td>";
        strContent += "        <td>" + data[key] + "</td>";
        strContent += "    </tr>";
    }
    strContent += "</table>";
    $("#ModalPopup .content").html(strContent);
    $("#ModalPopup").fadeIn(330);
}

function updateRelais(data) {
    if (data['state'] == "True") {
        $("#" + data['RelaisID']).addClass("active");
    } else {
        $("#" + data['RelaisID']).removeClass("active");
    }
}

(function ($) {
    $(".MobileMenu").click(function () {
        if ($("#MainNavigationFrame").height() < 10) {
            $("#MainNavigationFrame").height($("#MainNavigationFrame")[0].scrollHeight);
        } else {
            $("#MainNavigationFrame").height("");
        }

    });    

    $("#ModalPopup .primaryButton").click(function (e) {
        e.preventDefault();
        $("#ModalPopup").fadeOut(330);
    });

    $(".openView").click(function (e) {
        e.preventDefault();
        objParameter = {
            EventID : $(this).data("eventid")
        };
    
        ajaxCall("http://" + window.location.host, "get_eventlog_detail", "POST", JSON.stringify(objParameter), "JSON", displayEventDetail);
    });

    $("select").change(function () {
        $(this).parent("div").children("label").focus(); // funktioniert nicht
    });

    //blockiert, die Eventweiterleitung ans Parent Element
    $(".relaisNavigation a").click(function (e) {
        e.stopPropagation();
    });

    $(".relayControl.relayHover").click(function (e) {
        e.preventDefault();

        objParameter = {
            RelaisID: $(this).attr("id"),
            gpio: $(this).data("gpio"),
            debug: $(this).hasClass("active")
        };

        ajaxCall("http://" + window.location.host, "relais_toggle", "POST", JSON.stringify(objParameter), "JSON", updateRelais);

    });
})(jQuery);


function windowsScrollEven() {

}

function responsiveJS() {
    if ($(".relayControl").length > 0) {
        $(".relayControl").height("");
        window.relaisHeight = 0;
        $(".relayControl").each(function (inex, ele) {
            if (window.relaisHeight < $(ele).height()) {
                window.relaisHeight = $(ele).height();
            }
        });

        $(".relayControl").height(window.relaisHeight);

    }
    
}



