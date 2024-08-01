using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Livraria.Application.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatorio")]
        [MinLength(3)]
        [MaxLength(40)]
        [DisplayName("Nome")]
        public string? Nome { get; set; }

        [JsonIgnore]
        public ICollection<LivroAutorDTO>? LivroAutores { get; set; }
    }
}

