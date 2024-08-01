using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Livraria.Application.DTOs
{
    public class LivroPrecoCanalVendaDTO
    {
        [Required(ErrorMessage = "Livro é obrigatório")]
        [DisplayName("Livro")]
        public int Livro_Codl { get; set; }

        [Required(ErrorMessage = "Canal de venda é obrigatório")]
        [DisplayName("Canal de Venda")]
        public int CanalVenda_CodCanal { get; set; }

        [Required(ErrorMessage = "Preço de venda é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço de venda deve ser maior que zero")]
        [DisplayName("Preço Venda")]
        public decimal PrecoVenda { get; set; }

        [JsonIgnore]
        public LivroDTO? Livro { get; set; }

        [JsonIgnore]
        public CanalVendaDTO? CanalVenda { get; set; }
    }
}
