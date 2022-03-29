(function (PL) {

    PL.NotificationType = { Success: "success", Warning: "warning", Danger: "danger", Info: "info" };


    PL.Row = '<li class="pl-ListItemRow" style=margin-top:15px>' +
        '                    <div class="table">' +
        '                       <div class="row">' +
        '                          <div class="col-md-6">' +
        '                             <input id="txtItemTitle" name="ItemTitle" required type="text" class="pl-ItemTitleInput pl-ItemTitle " placeholder="Enter Item Title">' +
        '                        </div>' +
        '                       <div class="col-md-6">' +
        '                          <input id="txtItemLink" name="ItemLink" type="text" class="pl-ItemLink pl-ItemLink " placeholder="Enter Item Link">' +
        '                     </div>' +
        '                </div>' +
        '               <div class="row">' +
        '                  <div class="col-md-9">' +
        '                    <textarea id="dvItemDescription" placeholder="Item Description" class="pl-ItemDescription  pl-borderRadius"></textarea>' +
        '               </div>' +
        '              <div class="col-md-3">' +
        '                 <div style="display: table; height: 96%; border: 1px solid #d3d3d3;width:100%;text-align:center">' +
        '                    <div style="display: table-cell; vertical-align: middle;">' +
        '<i class="fas fa-minus-circle pl-removeImage" title="Click to remove image"></i>' +
        '                       <div id="dvImagePreview" class="pl-ImagePreview">' +
        '                         Image Preview' +
        '                     </div>' +
        '</div >' +
        '                </div>' +
        '           </div>' +
        '      </div>' +
        ' </div>' +
        '<div class="row">' +
        '   <div class="col-md-12" style="text-align:right">' +
        '      <div id="dvImageContainer" class="pl-ImageContainer">' +
        '         <img src="' + window.applicationBaseUrl + 'Images/addImage.png" alt="Click here to add image" title="Click here to add image" />' +
        '    </div>' +
        '   <div class="pl-handlecell" style="display:inline;padding-left:10px;">' +
        '       <img src="' + window.applicationBaseUrl + 'Images/move-arrows.svg" class="handle ui-sortable-handle" title="Click here to drag item">' +
        '      <span class="glyphicon glyphicon-move handle ui-sortable-handle"></span>' +
        ' </div>' +
        '<div class="pl-deleteitem"  style="display:inline">' +
        '   <img src="' + window.applicationBaseUrl + 'Images/delete.png" alt="Click here to delete item" title="Click here to delete item" />' +
        '                           </div>' +
        '                      </div>' +
        '                 </div>' +
        '        </li>';



    PL.AddNewRow = function () {
        // $(".pl-mainlist").append(row);
        $(PL.Row).appendTo(".pl-mainlist").hide().slideDown("fast");
    }


    PL.GetListInfo = function () {

        var listObject = {};
        listObject.ListInfo = {};
        listObject.OtherListInfo = {};
        if ($("#txtId").length > 0) {
            listObject.ListInfo.Id = $("#txtId").val();
        }
        listObject.ListInfo.ListTitle = $("#txtListTitle").val();
        listObject.ListInfo.Category = $("#selCategory").val();
        // listObject.ListInfo.SubCategory = $("#selSubCategory").val();
        var tags = $("#txtTags").val();//tagsinput('items');
        listObject.OtherListInfo.FeaturedImageUrl = $("#txtFeaturedImageUrl").val();



        listObject.ListInfo.Tags = tags;

        listObject.ListItemDetails = [];


        $(".pl-ListItemRow").each(function () {
            var listItem = {};
            listItem.ItemTitle = $(this).find("#txtItemTitle").val();
            listItem.LinkUrl = $(this).find("#txtItemLink").val();
            listItem.ItemDescription = $(this).find("#dvItemDescription").val().trim();
            listItem.ItemImageUrl = $(this).find("#dvImagePreview .pl-ThumbImage").attr("src");
            listObject.ListItemDetails.push(listItem);
        });

        console.log(listObject);

        return listObject;

    }

    PL.Notify = function (notificationMessage, notificationType) {
        $.notify({
            // options
            message: notificationMessage
        }, {
                // settings
                type: notificationType,
                animate: {
                    enter: 'animated bounceInDown',
                    exit: 'animated bounceOutUp'
                }
            });
    }

    PL.ShowModal = function () {
        $("body").addClass("loading");
    }

    PL.HideModal = function () {
        $("body").removeClass("loading");
    }



    PL.ValidateForm = function () {

        var isValid = true;
        if (!PL.ValidateField(".pl-ItemTitle")) {
            isValid = false;
        }
        if (!PL.ValidateField("#txtListTitle")) {
            isValid = false;
        }

        return isValid;

    }

    PL.ValidateField = function (selector) {

        var isValid = true;

        $(selector).each(function () {

            if ($(this).val() == "") {
                $(this).addClass("error");
                $(this).focus();
                isValid = false;
            }
            else {
                $(this).removeClass("error");
            }

        });

        return isValid;
    }


    PL.PageLoad = function () {

        //Enabling jqeury tooltip
        $(document).tooltip();


        //Making list items sortable
        $(".pl-mainlist").sortable({
            //  items: "div:not(.pl-ListItemRow)",
            handle: '.handle',
            placeholder: 'pl-sortplaceholder'
        });


        // On click for image url adding
        var clickedImageContainer;
        $("body").on("click", ".pl-ImageContainer", function () {

            $("#dvImageUrlWrapper").addClass("pl-overlay").show();
            clickedImageContainer = this;

        });


        // Image submit
        $("#btnSubmit").click(function () {

            //attach the image
            $(clickedImageContainer).closest("li").find(".pl-ImagePreview").html("<img class='pl-ThumbImage' src='" + $("#txtImageUrl").val() + "' />");
            //hide the wrapper
            $("#dvImageUrlWrapper").hide();
            //make the image url blank
            $("#txtImageUrl").val("");
        });


        //Delete Item Click
        $("body").on("click", ".pl-deleteitem", function () {

            if ($(this).closest("li").parent().find("li").length != 1) {
                $(this).closest("li").slideUp(200, function () {
                    $(this).remove();
                });
            }

        });


        // Remove image button click
        $("body").on("click", ".pl-removeImage", function () {
            $(this).parent().find(".pl-ImagePreview").html("Image Preview");            // $(this).hide();

        });



    }



})(window.PL = window.PL || {});


$(document).ready(PL.PageLoad);

