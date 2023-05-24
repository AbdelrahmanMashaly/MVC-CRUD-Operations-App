using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.PL.ViewModel
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Is required")]
		[EmailAddress(ErrorMessage = "Must Enter Valid Email address")]
		public string Email { get; set; }
		[Required(ErrorMessage = " You must enter password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
