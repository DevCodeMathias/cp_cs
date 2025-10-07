using System.ComponentModel.DataAnnotations;

namespace checkpoint__10072025.Models
{
    public class Cliente
    {
        public long Id { get; set; }

        [Required, StringLength(120)]
        public string Nome { get; set; } = "";

        [Required, EmailAddress, StringLength(120)]
        public string Email { get; set; } = "";

        [Phone, StringLength(20)]
        public string? Telefone { get; set; }
    }
}
