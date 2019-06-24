/// <reference path="base_functions.js" />
(function ($) {
    $(".MobileMenu").click(function () {
        if ($("#MainNavigationFrame").height() < 10) {
            $("#MainNavigationFrame").height($("#MainNavigationFrame")[0].scrollHeight);
        } else {
            $("#MainNavigationFrame").height("");
        }

    });    
})(jQuery);


function windowsScrollEven() {

}

function responsiveJS() {
    
}



