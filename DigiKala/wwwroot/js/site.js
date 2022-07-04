// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function Success(inputName, modalName, res, toastMessage) {
    $(inputName).text(res);
    $(modalName).removeClass("remodal-is-opened");
    $(modalName).addClass("remodal-is-closed");
    $("div.remodal-is-opened").addClass("remodal-is-closed");
    $("div.remodal-is-opened").css("display", "none");
    $("div.remodal-is-opened").removeClass("remodal-is-opened");
    iziToast.success({
        message: toastMessage,
        rtl: true,
        position: 'bottomCenter',
        timeout: 3000
    });
}
function WarningMessage(message) {
    iziToast.warning({
        message: message,
        rtl: true,
        position: 'bottomCenter',
        timeout: 3000
    });
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
                    Success("#nameInp", "#nameModal", res, "نام و نام خانوادگی شما با موفقیت ویرایش شد");
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
                    Success("#nationalNumberInp", "#nationalnumberModal", res, "کد ملی شما با موفقیت ویرایش شد");
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
                    Success("#emailInp", "#emailModal", res, "ایمیل شما با موفقیت ویرایش شد");
                },
                error: function (error) {
                    WarningMessage(error.responseText);
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
                    Success("#phoneNumberInp", "#phoneNumberModal", res, "شماره همراه شما با موفقیت ویرایش شد");
                },
                error: function (error) {
                    WarningMessage(error.responseText);
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
                    Success("#birthDateInp", "#birthDateModal", res, "تاریخ تولد شما با موفقیت ویرایش شد");
                },
                error: function (error) {
                    WarningMessage(error.responseText);
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
                    iziToast.success({
                        message: "رمز عبور شما با موفقیت ویرایش شد از این پس می توانید با استفاده از این رمز عبور وارد سایت شوید",
                        rtl: true,
                        position: 'bottomCenter',
                        timeout: 3000
                    });
                    $("#userPasswordModal").removeClass("remodal-is-opened");
                    $("#userPasswordModal").addClass("remodal-is-closed");
                    $("div.remodal-is-opened").addClass("remodal-is-closed");
                    $("div.remodal-is-opened").css("display", "none");
                    $("div.remodal-is-opened").removeClass("remodal-is-opened");
                },
                error: function (error) {
                    WarningMessage(error.responseText);
                }
            });
        }
    });
});


/*********************Email And Phone Number Existance *****************************/

$("#btnCreateUser").click(function (e) {
    $("#frmCreateUser").validate();
    var isValidForm = $("#frmCreateUser").valid();
    var isValidEmail = false;

    if (isValidForm) {
        e.preventDefault();
        if ($("#CreateUserVM_Email").val() != "") {
            $.ajax({
                type: "Get",
                url: "/Admin/Users/IsExistEmailOrPhoneNumber?email=" + $("#CreateUserVM_Email").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این ایمیل از قبل وجود دارد", "error");
                }
                else {
                    if ($("#CreateUserVM_PhoneNumber").val() == "") {
                        $("#frmCreateUser").submit();
                    }
                    else {
                        isValidEmail = true;
                    }
                }
            });
        }
        if ($("#CreateUserVM_PhoneNumber").val() != "") {
            $.ajax({
                type: "Get",
                url: "/Admin/Users/IsExistEmailOrPhoneNumber?phoneNumber=" + $("#CreateUserVM_PhoneNumber").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این شماره تلفن از قبل وجود دارد", "error");
                }
                else {
                    if ($("#CreateUserVM_Email").val() != "") {
                        if (isValidEmail) {
                            $("#frmCreateUser").submit();
                        }
                    }
                    else {
                        $("#frmCreateUser").submit();
                    }

                }
            });
        }
    }
});
/***********************************Edit User Informations From Admin************************************/
function GetUserInformationsForEdit(firstName, lastName, email, phoneNumber, avatarName, userId, roleId) {

    localStorage.setItem("UserEmail", email);
    localStorage.setItem("PhoneNumber", phoneNumber);
    $("#userIdInp").val(userId);

    var inputs = $(".editRoleRadio");
    for (var i = 0; i < inputs.length; i++) {
        if ($(inputs[i]).val() == roleId) {
            $(inputs[i]).attr("checked", "true");
        }
    }

    $("#EditUserVM_FirstName").val(firstName);
    $("#EditUserVM_LastName").val(lastName);
    $("#EditUserVM_Email").val(email);
    $("#EditUserVM_PhoneNumber").val(phoneNumber);
    $("#editImgAvatar").attr("src", "/UserAvatar/" + avatarName);
    $("#modalEditUser").modal('show');
}
/*****************************Edit User PhoneNumber and Email Existence******************************/

$("#btnEditUser").click(function (e) {
    $("#frmEditUser").validate();
    var isValidForm = $("#frmEditUser").valid();
    var isValidEmailInEdit = false;

    if (isValidForm) {
        e.preventDefault();
        if ($("#EditUserVM_Email").val() != "" && $("#EditUserVM_Email").val() != localStorage.getItem("UserEmail")) {
            $.ajax({
                type: "Get",
                url: "/Admin/Users/IsExistEmailOrPhoneNumber?email=" + $("#EditUserVM_Email").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این ایمیل از قبل وجود دارد", "error");
                }
                else if ($("#EditUserVM_PhoneNumber").val() == "") {
                    $("#frmEditUser").submit();
                }
                else if (!($("#EditUserVM_PhoneNumber").val() != "" && $("#EditUserVM_PhoneNumber").val() != localStorage.getItem("PhoneNumber"))) {
                    $("#frmEditUser").submit();
                }
                else {
                    isValidEmailInEdit = true;
                }
            });
        }

        if ($("#EditUserVM_PhoneNumber").val() != "" && $("#EditUserVM_PhoneNumber").val() != localStorage.getItem("PhoneNumber")) {
            $.ajax({
                type: "Get",
                url: "/Admin/Users/IsExistEmailOrPhoneNumber?phoneNumber=" + $("#EditUserVM_PhoneNumber").val()
            }).done(function (result) {
                if (result == "true") {
                    sweetAlert("پیغام", "این شماره تلفن از قبل وجود دارد", "error");
                }
                else if ($("#EditUserVM_Email").val() == "" || $("#EditUserVM_Email").val() == localStorage.getItem("UserEmail")) {
                    $("#frmEditUser").submit();
                }
                else if (isValidEmailInEdit) {
                    $("#frmEditUser").submit();
                }
            });
        }
    }
});

