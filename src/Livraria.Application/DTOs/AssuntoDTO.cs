using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Livraria.Application.DTOs
{
    public class AssuntoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrição obrigatória")]
        [MinLength(3)]
        [MaxLength(40)]
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [JsonIgnore]
        public ICollection<LivroAssuntoDTO>? LivroAssuntos { get; set; }
    }
}

