using FoolProof.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.ViewModels.User
{
    public class UsersInformationsForShowInAdminViewModel
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? NationalNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public string? RoleName { get; set; }
    }
    public class CreateUserViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        [RequiredIfEmpty("PhoneNumber", ErrorMessage = "حداقل یک از موارد ایمیل یا شماره همراه را وارد کنید")]
        public string? Email { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [RequiredIfNotEmpty("Email", ErrorMessage = "در صورت وارد کردن ایمیل رمز عبور را باید وارد کنید")]
        public string? Password { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? LastName { get; set; }

        [Display(Name = "شماره همراه")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [RequiredIfEmpty("Email", ErrorMessage = "حداقل یک از موارد ایمیل یا شماره همراه را وارد کنید")]
        public string? PhoneNumber { get; set; }
        public IFormFile? UserAvatar { get; set; }
    }
}
