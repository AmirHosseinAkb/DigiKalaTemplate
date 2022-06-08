using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.ViewModels.User
{
    public class RegisterAndLoginViewModel
    {
        [Display(Name = "ایمیل یا شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EmailOrPhoneNumber { get; set; }
    }
    public class LoginViewModel
    {
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="تکرار رمز عبور با رمز عبور مطابقت ندارد")]
        public string RePassword { get; set; }
    }

    public class ForgetPasswordViewModel
    {
        [Display(Name = "ایمیل یا شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EmailOrPhoneNumber { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "تکرار رمز عبور با رمز عبور مطابقت ندارد")]
        public string RepeatNewPassword { get; set; }
    }
    public class VerificationViewModel
    {
        [Display(Name = "کد تایید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(10000,99999,ErrorMessage ="کد تایید پنج رقمی را به صورت صحیح وارد کنید")]
        public int VerificationCode { get; set; }
    }
}
