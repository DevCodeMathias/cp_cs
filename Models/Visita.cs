using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace checkpoint__10072025.Models
{
    public class Visita
    {
        public long Id { get; set; }
        [Required] public long ImovelId { get; set; }
        [Required] public DateTime Inicio { get; set; }
        [Required] public DateTime Fim { get; set; }

        [ForeignKey(nameof(ImovelId))] public Imovel? Imovel { get; set; }
    }
}
