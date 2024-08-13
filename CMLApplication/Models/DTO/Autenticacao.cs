using System.ComponentModel.DataAnnotations;

namespace CMLApplication.Models.DTO
{
    public class Autenticacao
    {
        [Required(ErrorMessage = "O Login deve ser preenchido.")]
        [MinLength(8, ErrorMessage = "Login deve possuir no mínimo 8 caracteres")]
        public string? Login { get; set; } = null;

        [Required(ErrorMessage = "A senha deve ser preenchida.")]
        [MinLength(8, ErrorMessage = "Senha deve possuir no mínimo 6 dígitos")]
        public string? Password { get; set; } = null;
    }
}
