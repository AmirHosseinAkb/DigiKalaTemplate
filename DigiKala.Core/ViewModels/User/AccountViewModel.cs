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
        [MinLength(8, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
