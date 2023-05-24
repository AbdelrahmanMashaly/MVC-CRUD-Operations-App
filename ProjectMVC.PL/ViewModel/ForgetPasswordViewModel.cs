using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.PL.ViewModel
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is required")]
		[EmailAddress(ErrorMessage = "Must Enter Valid Email address")]
		public string Email { get; set; }
	}
}
