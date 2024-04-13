// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetCreateForumVC(IdInitiator, IdCategory, IdParent = null) {
    GetActionForumVC("/Forum/GetCreateForumVC",
        {
            idInitiator: IdInitiator,
            idCategory: IdCategory,
            idParent: IdParent
        },
        IdParent == null ? "Создать вложенный форум" : "Создать форум");
}

function GetCreateTopicVC(IdInitiator, IdForum, IdForumCat) {
    GetActionForumVC("/Topic/GetCreateTopicVC",
        {
            idInitiator: IdInitiator,
            idForum: IdForum,
            idForumCat: IdForumCat
        },
        "Создать вопрос");
}

function GetActionForumVC(urlVC, dataObject, headerText) {
    $.ajax({
        type: "GET",
        url: urlVC,
        data: dataObject

    }).done(function (data, statusText, xhdr) {
        console.log("Done");
        $("#pnlOffcanvasBody").html(data);
        $("#offcanvasActionForumLabel").text(headerText);
    }).fail(function (xhdr, statusText, errorText) {
        console.log("Failed");
        $("#pnlOffcanvasBody").text(JSON.stringify(xhdr));
    });
}