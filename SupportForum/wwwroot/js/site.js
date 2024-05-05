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
        IdParent == null ? "Создать форум" : "Создать вложенный форум");
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

function GetDeleteForumVC(IdForum, IdForumCat) {
    GetActionForumVC("/Forum/GetDeleteForumVC",
        {
            idForum: IdForum,
            idForumCat: IdForumCat
        },
        "Вы уверены, что хотите удалить этот форум?");
}
function GetDeleteTopicVC(IdTopic, IdForumCat) {
    GetActionForumVC("/Topic/GetDeleteTopicVC",
        {
            idTopic: IdTopic,
            idForumCat: IdForumCat
        },
        "Вы уверены, что хотите удалить этот вопрос?");
}

function GetEditForumVC(IdForum, IdForumCat) {
    GetActionForumVC("/Forum/GetEditForumVC",
        {
            idForum: IdForum,
            idForumCat: IdForumCat
        },
        "Редактировать форум");
}

function GetEditTopicVC(IdTopic, IdForumCat) {
    GetActionForumVC("/Topic/GetEditTopicVC",
        {
            idTopic: IdTopic,
            idForumCat: IdForumCat
        },
        "Редактировать вопрос");
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