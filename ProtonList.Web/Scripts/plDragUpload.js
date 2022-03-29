
(function ($) {

    $.fn.DragUpload = function (options) {

        // Default options
        var settings = $.extend({
            fileuploadId: 'pl-imgUpload',
            allowedExtentions: ['png', 'jpg', 'jpeg'],
            selector: "#dvImagePreview"
        }, options);

        PLDragUpload.init(this, settings);

        // Apply options
        return;
    };


}(jQuery));

(function (PLDragUpload) {


    PLDragUpload.init = function (dropArea, settings) {


        $(document.body).on('dragenter', settings.selector, function (e) {
            e.preventDefault();
            $(this).css('background', '#BBD5B8');
        });

        $(document.body).on('dragover', settings.selector, function (e) {
            e.preventDefault();
        });

        $(document.body).on('drop', settings.selector, function (e) {
            $(this).css('background', '#D8F9D3');
            e.preventDefault();
            console.log("Dropped");
            var fileList = e.originalEvent.dataTransfer.files;
            var fileExtention = fileList[0].name.split('.').pop();
            if ($.inArray(fileExtention, settings.allowedExtentions) != -1) {
                PLDragUpload.uploadImage(fileList, this);
            }
            else {
                PL.Notify("Please upload file of type" + settings.allowedExtentions.toString(), PL.NotificationType.Warning);
            }
        });

        var selectedRow;
        $(document.body).on('click', settings.selector, function (e) {
            selectedRow = this;
            $("#" + settings.fileuploadId).click();

        });


        //to handle file upload control
        $("#" + settings.fileuploadId).change(function () {

            var fileList = this.files;
            var fileExtention = fileList[0].name.split('.').pop();
            if ($.inArray(fileExtention, settings.allowedExtentions) != -1) {
                PLDragUpload.uploadImage(fileList, selectedRow);
            }
            else {
                PL.Notify("Please upload file of type" + settings.allowedExtentions.toString(), PL.NotificationType.Warning);
            }

            $("#" + settings.fileuploadId).val('');// set the value to empty of myfile control. 
        });



    }


    PLDragUpload.uploadImage = function (imageFile, dropArea) {

        var antiForgeryToken = $("input[name=__RequestVerificationToken]").val();
        var postUrl = "UploadFile";

        // jQuery.postJSON(postUrl, imageFile, PLDragUpload.imageUploadSuccess, PLDragUpload.imageUploadError, antiForgeryToken, "text");
        var fileData = new FormData();
        fileData.append(imageFile[0].name, imageFile[0]);

        $.ajax({
            type: "POST",
            url: postUrl,
            contentType: false,
            processData: false,
            data: fileData,
            success: function (result) {
                PLDragUpload.imageUploadSuccess(result, dropArea);
            },
            error: function (xhr, status) {
                var err = "Error " + " " + status;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });

    }

    PLDragUpload.imageUploadSuccess = function (data, dropArea) {
        $(dropArea).html("<img class='pl-ThumbImage' src='" + window.location.origin + window.applicationBaseUrl + data + "' />");
    }

    PLDragUpload.imageUploadError = function (args, e) {

        console.log("Error");
        console.log(args.responseText);
        console.log(e);

    }




})(window.PLDragUpload = window.PLDragUpload || {});
