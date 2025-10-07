using System.ComponentModel.DataAnnotations;

namespace checkpoint__10072025.Models
{
    public class Produto
    {
        public long Id { get; set; }

        [Required, StringLength(80)]
        public string SKU { get; set; } = ""; // Único

        [Required, StringLength(150)]
        public string Nome { get; set; } = "";

        [Range(0, 999999)]
        public decimal Preco { get; set; }
    }
}
