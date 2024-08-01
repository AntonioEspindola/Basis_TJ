using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Livraria.Application.DTOs
{
    public class LivroAutorAssuntoDTO
    {
        public int LivroId { get; set; }

        [DisplayName("Título")]
        public string? Titulo { get; set; }

        [DisplayName("Editora")]
        public string? Editora { get; set; }

        [DisplayName("Edição")]
        public int Edicao { get; set; }

        [DisplayName("Ano de Publicação")]
        public int AnoPublicacao { get; set; }

        public int AutorId { get; set; }

        [DisplayName("Nome do Autor")]
        public string? AutorNome { get; set; }

        public int AssuntoId { get; set; }

        [DisplayName("Descrição do Assunto")]
        public string? AssuntoDescricao { get; set; }
    }
}
