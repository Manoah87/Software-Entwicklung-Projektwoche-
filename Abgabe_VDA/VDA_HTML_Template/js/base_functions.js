var g_pageObj;

if (window.jQuery) {
    $(function () {

        //prevent or remove traget blank =>  e.preventDefault();

        var pageProperties = function (windowWidth, windowHeight, isDebug) {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.isDebug = isDebug;
            this.isMobile = false;

            this.isWindowsSafari = false;
            this.scrollPos = 0;

            this.setIsMobile = function () {
                var mobile = navigator.userAgent.match(/(iPhone|iPod|Android|BlackBerry|Windows Phone)/);
                if (mobile !== undefined && mobile !== null) {
                    this.isMobile = true;
                } else {
                    this.isMobile = false;
                }
            };

            this.updateWindwosResolution = function () {
                this.windowWidth = window.innerWidth;
                this.windowHeight = window.innerHeight;
            };

            this.setisWindowSafari = function () {
                this.isWindowsSafari = (navigator.userAgent.match(/^(?=.*\bWindows\b)(?=.*\bSafari\b)(?!.*\bEdge\b)/) != null);
            };

            this.renderDebugInfo = function () {
                if ($("#DebugInfo").length === 0)
                    $("body").before("<div id=\"DebugInfo\"></div>");

                var strDebugInfo = "<span>Width:</span>" + this.windowWidth + "<br />";
                strDebugInfo += "<span>Height:</span>" + this.windowHeight + "<br />";
                strDebugInfo += "<span>isMobile:</span>" + this.isMobile.toString() + "<br />";
                strDebugInfo += "<span>scrollPos:</span>" + this.scrollPos + "<br />";

                if ($("#DebugInfo").length > 0) {
                    $("#DebugInfo").html(strDebugInfo);
                }
            }
        }

        var isDebugEnable = window.location.href.indexOf("localhost") > -1;

        g_pageObj = new pageProperties(window.innerWidth, window.innerHeight, isDebugEnable);
        g_pageObj.setIsMobile();
        g_pageObj.setisWindowSafari();


        $(window).scroll(function () {
            g_pageObj.scrollPos = $(document).scrollTop();
            windowsScrollEven();
        });

        delete window.alert;

        if (typeof Window === "function") {
            Window.prototype.alert = function () {
                toastr.warning(arguments[0]);
            };
        }

        responsiveJS();
    });
} else {
    if (g_pageObj.isDebug)
        toastr.error("jquery is not implement", jQuery);
}

$(window).resize(function () {
    if (typeof g_pageObj.updateWindwosResolution == "function") {
        g_pageObj.updateWindwosResolution();
    }

    if (typeof responsiveJS == "function") {
        responsiveJS();
    } else {
        if (g_pageObj.isDebug)
            toastr.warning("responseiveJS not exist");
    }
});

function scrollToEvent($ele, scrollTopVal) {
    scrollTopVal = scrollTopVal || 0;

    if ($ele != null && $ele.length > 0) {
        jQuery('body,html').animate({
            scrollTop: ($ele.offset().top - 20 - $("header").height())
        }, 1000);
    } else {
        if (scrollTopVal != null) {
            jQuery("body,html").animate({
                scrollTop: scrollTopVal
            }, 1000);
        }
    }
}

function slidDown(ele) {
    if ($(ele).height() == 0) {
        $(ele).height($(ele)[0].scrollHeight);
    } else {
        $(ele).height(0);
    }
}

function slideShowJQ(ele, fn, strDirection, timeMS) {
    if (fn == "show") {
        $(ele).show("slide", { trDirection: strDirection }, timeMS);
    } else {
        $(ele).hide("slide", { trDirection: strDirection }, timeMS);
    }
}

function ajaxCall(pageUrl, webmethod, strType, data, dataType, successCallback) {
    $.ajax({
        type: strType,
        url: pageUrl + "/" + webmethod,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: dataType,
        success: successCallback,
        error: function (e) {
            console.log("Error in Webmethod [pageUrl + " / " + webmethod]: " + e.status + " " + e.statusText + ": " + e.responseText);
        }
    });
}

String.prototype.format = function () {
    var formatted = this;
    for (var i = 0; i < arguments.length; i++) {
        var regexp = new RegExp('\\{' + i + '\\}', 'gi');
        formatted = formatted.replace(regexp, arguments[i]);
    }
    return formatted;
};

String.prototype.dateTimeReviver = function () {
    if (isNaN(Date.parse(this)) == false)
        return new Date(this);

    var value = this.replace("+0200", "");

    value = /-?[0-9]\d*(\.\d+)?/.exec(value);
    if (value) {
        return new Date(+value[0]);
    }

    return value;
};

String.prototype.isMail = function () {
    var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    return pattern.test(this);
};

function validateDate(inDatum) {
    // var re = /^([0-9]{2}[.|\/]{1}[0-9]{2}[.|\/]{1}[0-9]{4})$/;
    var re = /^(?:(?:31(\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
    if (inDatum == null) {
        return false;
    }
    return re.test(inDatum.trim());

}

function isNumeric(n) { return /^-?[\d.]+(?:e-?\d+)?$/.test(n); }


window.onerror = function (msg, url, line, col, error) {
    if (g_pageObj != null && g_pageObj.isDebug)
        toastr.error(msg, "Global Error");
    // Note that col & error are new to the HTML 5 spec and may not be 
    // supported in every browser.  It worked for me in Chrome.
    var extra = !col ? '' : ' column: ' + col;
    extra += !error ? '' : ' error: ' + error;

    var date = new Date();

    // You can view the information in an alert to see things working like this:
    var errorMsg = date.toString() + "Error: " + msg + " url: " + url + " line: " + line + extra;

    var logfileName = "global_js_bugs_{%DateNow%}.log";

    if (g_pageObj != null && g_pageObj.isDebug == true) {
        console.log(JSON.stringify(errorMsg));
    }
};

if (typeof Window === "function") {
    Window.prototype.alert = function () {
        switch (arguments.length) {
            case 0:
                toastr.warning("Funktion alert ist leer");
                break;
            case 1:
                toastr.warning(arguments[0]);
                break;
            case 2:
                toastr.warning(arguments[0], arguments[1]);
                break;
            default:
                toastr.warning(arguments[0]);
        }
    };

} 