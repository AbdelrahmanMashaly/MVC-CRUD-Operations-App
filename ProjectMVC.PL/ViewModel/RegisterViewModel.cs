using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.PL.ViewModel
{
	public class RegisterViewModel
	{
		public string Fname { get; set; }
		public string Lname { get; set; }

		[Required(ErrorMessage ="Email Is required")]
		[EmailAddress(ErrorMessage ="Must Enter Valid Email address")]
		public  string Email { get; set; }
		[Required(ErrorMessage =" You must enter password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage ="You must confirm the password")]
		[Compare("Password",ErrorMessage ="Doesnot match the password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
