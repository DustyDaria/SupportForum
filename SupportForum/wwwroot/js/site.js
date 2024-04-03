// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function GetCreateForumVC(IdInitiator, IdCategory, IdParent = null) {
    $.ajax({
        type: "GET",
        url: "/Forum/GetCreateForumVC",
        data: {
            IdInitiator, IdCategory, IdParent
        }

    }).done(function (data, statusText, xhdr) {
        console.log("Done");
        $("#pnlOffcanvasBody").html(data);
        if (IdParent != null) $("#offcanvasActionForumLabel").text("Создать вложенный форум");
        else $("#offcanvasActionForumLabel").text("Создать форум");
    }).fail(function (xhdr, statusText, errorText) {
        console.log("Failed");
        $("#pnlOffcanvasBody").text(JSON.stringify(xhdr));
    });
}