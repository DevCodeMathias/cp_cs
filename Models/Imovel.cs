using System.ComponentModel.DataAnnotations;

namespace checkpoint__10072025.Models
{
    public class Imovel
    {
        public long Id { get; set; }


        [Required, StringLength(150)]
        public string Titulo { get; set; } = "";

        [Required, StringLength(200)]
        public string Endereco { get; set; } = "";

        [Range(0, 999999)]
        public decimal Valor { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
