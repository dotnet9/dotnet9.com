function addNewComment(data) {
    return $.ajax({
        type: "POST",
        url: '/api/comment',
        data: data,
        dataType: 'json',
        contentType: 'application/json;charset=utf-8'
    });
}



$(function () {

    var source = document.getElementById("comment-template").innerHTML;
    var template = Handlebars.compile(source);


    $("#addNewCommentBtn").click(function (e) {
        e.preventDefault();
        var data = JSON.stringify({
            url: window.location.pathname,
            parentId: null,
            content: $("#commentInput").val(),
            userName: Math.random().toString(36).substring(2, 10) + Math.random().toString(36).substring(2, 5),
            email: Math.random().toString(36).substring(2, 10) + Math.random().toString(36).substring(2, 5)
        });


        $.when(addNewComment(data)).then(function (response) {
            if (response.error == false) {
                var $commentHtml = template(response.result.data);
                $(".comments").append($commentHtml);
                $("#commentInput").val('');
            } else {
                console.log("An error has occured");
            }
        }).fail(function (err) {
            console.log(err);
        })
    });


    $(document).on("click", ".reply-link", function (e) {
        e.preventDefault();
        var $self = $(this);
        var $commentListItem = $self.parents(".comment-body");
        var replySource = document.getElementById("reply-template").innerHTML;
        $commentListItem.after(replySource);

        $(document).find(".hidParentId").val($self.attr("href"));

    });



    $(document).on("click", ".addReplyBtn", function (e) {
        e.preventDefault();
        var $self = $(this);
        var $replyInput = $(document).find("#replyInput");
        var parentId = $(document).find(".hidParentId").val();
        var $repliesUL = $(document).find("ul[data-parentid='" + parentId + "']");
        var $commentBody = $(document).find("[data-id='" + parentId + "']");
        var replyULCount = $repliesUL.length;



        var data = JSON.stringify({
            parentId: parentId,
            commentText: $replyInput.val(),
            username: Math.random().toString(36).substring(2, 10) + Math.random().toString(36).substring(2, 5)
        });

        $.when(addNewComment(data)).then(function (response) {
            if (response.error == false) {
                var commentHtml = template(response.result);

                if (replyULCount > 0) {

                    $repliesUL.append(commentHtml);
                } else {

                    $commentBody.append("<ul class='replies'></ul>")
                        .append(commentHtml);
                }

                $(document).find(".reply-form").remove();

            } else {
                console.log("An error has occured");
            }
        }).fail(function (err) {
            console.log(err);
        })

    })
});