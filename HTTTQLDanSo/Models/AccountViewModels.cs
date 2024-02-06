using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace HTTTQLDanSo.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
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
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class AccountViewModel
    {
        public string FullName
        {
            get { return $"{LastName} {FirstName}"; }
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string RoleId { get; set; }

        public string AllRoles { get; set; }

        public string AllAddress { get; set; }

        public string WorkName { get; set; }

        public string RegionID { get; set; }

        public string ProvinName { get; set; }

        public string DistrictName { get; set; }

        public string RegionName { get; set; }

        public string Address_Name { get; set; }

        public string Full_Address { get; set; }
    }

    public class EditAccountViewModel : RegisterAccountViewModel
    {
        public string Id { get; set; }
    }

    public class RegisterAccountViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập quyền.")]
        public IList<string> SelectedRoleNames { get; set; }

        public IList<RoleViewModel> Roles { get; set; }

        [MustHaveWorkerIds(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        public List<int> WorkerIds { get; set; }

        public List<System.Web.Mvc.SelectListItem> Workers { get; set; }

        public string RegionID { get; set; }

        public string ProvinId { set; get; }

        public string DistrictId { set; get; }

        [BindNever]
        public List<System.Web.Mvc.SelectListItem> Provinces { get; set; } = new List<System.Web.Mvc.SelectListItem>();

        [BindNever]
        public List<System.Web.Mvc.SelectListItem> Districts { get; set; } = new List<System.Web.Mvc.SelectListItem>();

        public List<System.Web.Mvc.SelectListItem> Regions { get; set; }
    }

    public class MustHaveWorkerIdsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is List<int> workerIds)
            {
                return workerIds.Count > 0;
            }

            return false;
        }
    }
}