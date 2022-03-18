using System.ComponentModel.DataAnnotations;

namespace GameApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo E-mail não pode ser vazio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha não pode ser vazio")]
        public string Senha { get; set; }
    }
}
