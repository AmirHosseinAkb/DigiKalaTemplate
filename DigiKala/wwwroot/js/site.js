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
            var data = $("#frmUserFullName").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/UserInformations/ConfirmUserFullName",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#nameInp", "#nameModal", res);
                }
            });
        }
    });
});

$(function () {
    $("#btnNationalNumber").click(function (e) {
        var isValidNAtionalNumber = $("#UserNationalNumberVM_NationalNumber").valid();
        if (isValidNAtionalNumber) {
            var data = $("#frmUserNationalNumber").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/UserInformations/ConfirmUserNationalNumber",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#nationalNumberInp", "#nationalnumberModal", res);
                }
            });
        }
    });
});
$(function () {
    $("#btnEmail").click(function (e) {
        var isValidEmail = $("#UserEmailVM_Email").valid();
        if (isValidEmail) {
            var data = $("#frmUserEmail").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/UserInformations/ConfirmUserEmail",
                data: data,
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
            var data = $("#frmUserPhoneNumber").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/UserInformations/ConfirmUserPhoneNumber",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#phoneNumberInp", "#phoneNumberModal", res);
                }
            });
        }
    });
});
$(function () {
    $("#btnBirthDate").click(function (e) {
        var isValidYear = $("#UserBirthDateVM_BirthYear").valid();
        var isValidMonth = $("#month").valid();
        var isValidDay = $("#UserBirthDateVM_BirthDay").valid();
        if (isValidYear && isValidMonth && isValidDay) {
            var data = $("#frmUserBirthDate").serialize();
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/UserInformations/ConfirmUserBirthDate",
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    Success("#birthDateInp", "#birthDateModal", res);
                }
            });
        }
    });
});
$(function () {
    $("#btnSavePassword").click(function (e) {
        var data = $("#frmUserPassword").serialize();
        var isValidCurrentPassword = $("#UserPasswordVM_CurrentPassword").valid();
        var isValidNewPassword = $("#UserPasswordVM_NewPassword").valid();
        var isValidRepeatNewPassword = $("#UserPasswordVM_RepeatNewPassword").valid();
        if (isValidCurrentPassword && isValidNewPassword && isValidRepeatNewPassword) {
            e.preventDefault();
            $.ajax({
                type: "Post",
                url: "/UserPanel/UserInformations/ConfirmUserPassword",
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: data,
                beforeSend: function (xhr) { xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val()); },
                success: function (res) {
                    alert(res);
                    $("#userPasswordModal").removeClass("remodal-is-opened");
                    $("#userPasswordModal").addClass("remodal-is-closed");
                    $("div.remodal-is-opened").addClass("remodal-is-closed");
                    $("div.remodal-is-opened").css("display", "none");
                    $("div.remodal-is-opened").removeClass("remodal-is-opened");
                },
                error: function (error) {
                    alert(error.responseText);
                }
            });
        }
    });
});
