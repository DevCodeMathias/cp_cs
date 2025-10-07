using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace checkpoint__10072025.Models
{
    public class Contrato
    {
        public long Id { get; set; }
        [Required] public long ClienteId { get; set; }
        [Required] public long ImovelId { get; set; }
        [Required] public DateTime Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public bool Ativo { get; set; } = true;

        [ForeignKey(nameof(ClienteId))] public Cliente? Cliente { get; set; }
        [ForeignKey(nameof(ImovelId))] public Imovel? Imovel { get; set; }
    }
}
