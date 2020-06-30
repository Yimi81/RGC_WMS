using System.ComponentModel.DataAnnotations;

namespace RGC.WMS.USA.Models
{
    public class LoginInputModel
    {

        public LoginInputModel()
        {
            Status = false;
        }

        [Required(ErrorMessage = "用户名不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "错误信息")]
        public string Msg { get; set; }

        [Required(ErrorMessage = "请填写验证码")]
        [Display(Name = "验证码")]
        public string ValidateCode { get; set; }

        public bool Status { get; set; }

        public bool RememberLogin { get; set; }


    }
}
