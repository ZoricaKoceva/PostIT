using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostIt.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Е-пошта")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомни го овој прелистувач?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Е-пошта")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Е-пошта")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [Display(Name = "Запомни ме?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
		[Required]
        [Display(Name = "Име и Презиме")]
        public string FirstSecondName { get; set; }

        [Phone]
        [Display(Name = "Телефонски број")]
        public string Phone { get; set; }
		
        [Required]
        [EmailAddress]
        [Display(Name = "Е-пошта")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврдете ја лозинката")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат!")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Е-пошта")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврдете ја лозинката")]
        [Compare("Password", ErrorMessage = "Лозинките не се совпаѓаат!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Е-пошта")]
        public string Email { get; set; }
    }
}
