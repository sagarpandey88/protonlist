$(document).ready(function () {


    $("img").on("error", function () {
        console.log("Its a bug in image");
        console.log(this.src);
        //    this.src = "../Images/ImageNotFound.jpg"
    });

    $(window).scroll(function () {

        //if ($(this).scrollTop() > 0) {
        // $('.pl-topNavigation').attr('style', 'display:none !important');
        //    //$('.pl-topNavigation').animate({
        //    //    display: "none !important"
        //    //}, 1500)

        //}
        //else {
        //    //   $('.pl-topNavigation').show("fast");
        //    $('.pl-topNavigation').attr('style', 'display:block !important');
        //}
    });

});



function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}




function TypeAheadSearch(inputSelector, sourceUrl, localData) {
    $.typeahead({
        input: inputSelector,
        minLength: 1,
        order: "asc",
        offset: false,
        hint: true,
        dynamic: true,
        delay: 300,
        source: {
            car: {
                data: localData,
                ajax: {
                    type: "POST",
                    url: sourceUrl,
                    data: {
                        keyword: '{{query}}'
                    }
                }
            }
        },
        callback: {
            onClick: function (node, a, item, event) {

                console.log(node)
                console.log(a)
                console.log(item)
                console.log(event)

                console.log('onClick function triggered');

            },
            onSubmit: function (node, form, item, event) {

                console.log(node)
                console.log(form)
                console.log(item)
                console.log(event)

                console.log('onSubmit override function triggered');


            }
        }
    });
}


jQuery.postJSON = function (url, data, successHandler, errorHandler, antiForgeryToken, dataType) {
    if (dataType === void 0) { dataType = "json"; }
    if (typeof (data) === "object") { data = JSON.stringify(data); }
    var ajax = {
        url: url,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: dataType,
        data: data,
        success: successHandler,
        error: errorHandler
    };
    if (antiForgeryToken) {
        ajax.headers = {
            "__RequestVerificationToken": antiForgeryToken
        };
    };

    return jQuery.ajax(ajax);
};