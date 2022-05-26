using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.ViewModels.User
{
    public  class UserInformationsViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? LastName { get; set; }

        [Display(Name = "کدملی")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? NationalNumber { get; set; }

        [Display(Name = "شماره همراه")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        public string? Email { get; set; }

        [Display(Name = "تاریخ تولد")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? BirthDate { get; set; }

        [Display(Name = "رمز عبور فعلی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string? RepeatNewPassword { get; set; }
    }
    public class UserFullNameViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }
    }
    public class UserNationalNumberViewMode
    {
        [Display(Name = "کدملی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string NationalNumber { get; set; }
    }
    public class UserEmailViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        public string Email { get; set; }
    }

    public class UserPhoneNumberViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PhoneNumber { get; set; }
    }

    public class UserBirthDateViewModel
    {
        [Display(Name = "سال تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BirthYear { get; set; }
        [Display(Name = "ماه تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BirthMonth { get; set; }
        [Display(Name = "روز تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BirthDay { get; set; }
    }

    public class UserPasswordViewModel
    {
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "رمز عبور جدید با تکرار آن مطابقت ندارد")]
        public string RepeatNewPassword { get; set; }
    }
}
