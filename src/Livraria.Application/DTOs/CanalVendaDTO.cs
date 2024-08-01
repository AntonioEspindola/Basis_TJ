using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Livraria.Application.DTOs
{
    public class CanalVendaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do canal é obrigatório")]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Nome Canal")]
        public string NomeCanal { get; set; }

        [JsonIgnore]
        public ICollection<LivroPrecoCanalVendaDTO>? LivroPrecoCanalVenda { get; set; }
    }

}

