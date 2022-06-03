// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(function () {
    $("#btnName").click(function (e) {
        if ($("#UserFullNameVM_FirstName").val() != "" && $("#UserFullNameVM_LastName").val() != "") {
            e.preventDefault();
            $.ajax({
                type: "Get",
                url: "/UserPanel/UserInformations/ConfirmUserFullName?firstName=" + $("#UserFullNameVM_FirstName").val() + "&lastName=" + $("#UserFullNameVM_LastName").val(),
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    $("#nameInp").text(res);
                    $("#nameModal").removeClass("remodal-is-opened");
                    $("#nameModal").addClass("remodal-is-closed");
                    $("div.remodal-is-opened").addClass("remodal-is-closed");
                    $("div.remodal-is-opened").css("display", "none");
                    $("div.remodal-is-opened").removeClass("remodal-is-opened");
                }
            });
        }
    });
});
// Write your JavaScript code.