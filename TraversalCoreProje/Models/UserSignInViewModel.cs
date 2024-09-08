using System.ComponentModel.DataAnnotations;

namespace TraversalCoreProje.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string? Password { get; set; }
    }
}
