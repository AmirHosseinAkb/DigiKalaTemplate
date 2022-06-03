// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function Success(inputName, modalName, res) {
    $(inputName).text(res);
    $(modalName).removeClass("remodal-is-opened");
    $(modalName).addClass("remodal-is-closed");
    $("div.remodal-is-opened").addClass("remodal-is-closed");
    $("div.remodal-is-opened").css("display", "none");
    $("div.remodal-is-opened").removeClass("remodal-is-opened");
}
$(function () {
    $("#btnName").click(function (e) {
        if ($("#UserFullNameVM_FirstName").val() != "" && $("#UserFullNameVM_LastName").val() != "") {
            e.preventDefault();
            $.ajax({
                type: "Get",
                url: "/UserPanel/UserInformations/ConfirmUserFullName?firstName=" + $("#UserFullNameVM_FirstName").val() + "&lastName=" + $("#UserFullNameVM_LastName").val(),
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#nameInp","#nameModal",res);
                }
            });
        }
    });
});

$(function () {
    $("#btnNationalNumber").click(function (e) {
        if ($("#UserNationalNumberVM_NationalNumber").val() != "") {
            e.preventDefault();
            $.ajax({
                type: "Get",
                url: "/UserPanel/UserInformations/ConfirmUserNationalNumber?nationalNumber=" + $("#UserNationalNumberVM_NationalNumber").val(),
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#nationalNumberInp", "#nationalnumberModal", res);
                }
            });
        }
    });
});
$(function () {
    $("#btnPhoneNumber").click(function (e) {
        if ($("#UserPhoneNumberVM_PhoneNumber").val() != "") {
            e.preventDefault();
            $.ajax({
                type: "Get",
                url: "/UserPanel/UserInformations/ConfirmUserPhoneNumber?phoneNumber=" + $("#UserPhoneNumberVM_PhoneNumber").val(),
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#phoneNumberInp", "#phoneNumberModal", res);
                }
            });
        }
    });
});

// Write your JavaScript code.