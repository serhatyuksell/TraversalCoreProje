using System.ComponentModel.DataAnnotations;

namespace TraversalCoreProje.Models
{
	public class UserRegisterViewModel
	{
		[Required(ErrorMessage ="Lütfen adınızı giriniz")]
        public string? Name { get; set; }
		[Required(ErrorMessage = "Lütfen Soyadınızı giriniz")]
		public string? Surname { get; set; }
		[Required(ErrorMessage = "Lütfen Kullanıcı adınızı giriniz")]
		public string? Username { get; set; }
		[Required(ErrorMessage = "Lütfen mail adresini giriniz")]
		public string? Mail { get; set; }
		[Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
		public string? Password { get; set; }
		[Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz")]
		[Compare("Password",ErrorMessage ="Şifreler Uyumlu değil")]
		public string? ConfirmPassword { get; set; }
		[Required(ErrorMessage="Lütfen Cinsiyeti Giriniz")]
		public string? Gender { get; set; }
	}
}
