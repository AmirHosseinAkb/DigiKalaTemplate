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
        var isValidFirstName = $("#UserFullNameVM_FirstName").valid();
        var isValidLastName = $("#UserFullNameVM_LastName").valid();
        if (isValidFirstName && isValidLastName) {
            e.preventDefault();
            $.ajax({
                type: "Get",
                url: "/UserPanel/UserInformations/ConfirmUserFullName?firstName=" + $("#UserFullNameVM_FirstName").val() + "&lastName=" + $("#UserFullNameVM_LastName").val(),
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#nameInp", "#nameModal", res);
                }
            });
        }
    });
});

$(function () {
    $("#btnEmail").click(function (e) {
        var isValidEmail = $("#UserEmailVM_Email").valid();
        if (isValidEmail) {
            e.preventDefault();
            $.ajax({
                type: "Get",
                url: "/UserPanel/UserInformations/ConfirmUserEmail?email=" + $("#UserEmailVM_Email").val(),
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#emailInp", "#emailModal", res);
                    window.location = "/RegisterAndLogin?emailChanged=true";
                },
                error: function (error) {
                    alert(error.responseText);
                }
            });
        }
    });
});
$(function () {
    $("#btnPhoneNumber").click(function (e) {
        var isValid = $("#UserPhoneNumberVM_PhoneNumber").valid();
        if (isValid) {
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
        }
    });
});
$(function () {
    $("#btnBirthDate").click(function (e) {
        var isValidYear = $("#UserBirthDateVM_BirthYear").valid();
        var isValidMonth = $("#month").valid();
        var isValidDay = $("#UserBirthDateVM_BirthDay").valid();
        if (isValidYear && isValidMonth && isValidDay) {
            e.preventDefault();
            $.ajax({
                type: "Get",
                url: "/UserPanel/UserInformations/ConfirmUserBirthDate?year=" + $("#UserBirthDateVM_BirthYear").val()
                    + "&month=" + $("#month").val()
                    + "&day=" + $("#UserBirthDateVM_BirthDay").val(),
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#birthDateInp", "#birthDateModal", res);
                }
            });
        }
    });
});

// Write your JavaScript code.