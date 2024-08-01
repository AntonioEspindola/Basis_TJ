using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Livraria.Application.DTOs
{
    public class LivroDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo é obrigatório")]
        [MinLength(3)]
        [MaxLength(40)]
        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Editora é obrigatório")]
        [MinLength(3)]
        [MaxLength(40)]
        [DisplayName("Editora")]
        public string Editora { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Edição deve ser número positivo")]
        [DisplayName("Edição")]
        public int Edicao { get; set; }

        [Required(ErrorMessage = "Ano de publicação é obrigatório")]
        [MinLength(4)]
        [MaxLength(4)]
        [DisplayName("Ano publicação")]
        public string AnoPublicacao { get; set; }
        [JsonIgnore]
        public ICollection<LivroAutorDTO>? LivroAutores { get; set; }
        [JsonIgnore]
        public ICollection<LivroAssuntoDTO>? LivroAssuntos { get; set; }

        public ICollection<LivroPrecoCanalVendaDTO> LivroPrecoCanalVenda { get; set; }
    }
}

