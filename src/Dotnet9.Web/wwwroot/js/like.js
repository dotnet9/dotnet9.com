$(function () {
    $(".diggit").click(function () {
        $.ajax({
            type: "POST",
            url: "/api/blogpost/like/" + $(".blogslug").val(),
            contentType: "application/json",
            success: function (res) {
                $("#diggnum").html(res.data)
            },
        });
    })
})