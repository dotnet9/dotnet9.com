$(function () {
    $(".diggit").click(function () {
        $.ajax({
            type: "POST",
            url: "http://localhost:5005/api/blogpost/like/" + $(".blogslug").val(),
            contentType: "application/json",
            success: function (res) {
                $("#diggnum").html(res.data)
            },
        });
    })
})